using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using GRC.Shared.UI.Dialogs;
using GRC.Shared.UI.Messages;
using GRC.Shared.UI.ViewModels.Dialogs;

namespace GRC.Shared.UI.Services;

/// <summary>
/// Base implementation for modal dialog orchestration with focus management and dialog stacking.
/// Derived classes can customize modal options and nested dialog behavior.
/// </summary>
public abstract class BaseDialogService
{
    private readonly Stack<Window> _dialogStack = new();
    private readonly IModalDialogService _modalDialogService;

    /// <summary>
    /// Gets the currently active dialog, or null if stack is empty.
    /// </summary>
    protected Window? CurrentDialog => _dialogStack.Count > 0 ? _dialogStack.Peek() : null;

    /// <summary>
    /// Initializes base dialog service with message handling for CloseDialogMessage.
    /// </summary>
    protected BaseDialogService(IMessenger messenger, IModalDialogService modalDialogService)
    {
        _modalDialogService = modalDialogService;
        messenger.Register<CloseDialogMessage>(this, (recipient, message) =>
        {
            CurrentDialog?.Close(message.Value);
        });
    }

    /// <summary>
    /// Displays a modal dialog for the given view model.
    /// </summary>
    private static void Diag(string msg)
    {
        try { File.AppendAllText("/tmp/grc-diag-file.log", $"[{DateTime.Now:HH:mm:ss.fff}] {msg}\n"); } catch { }
    }

    public async Task<bool> ShowDialogAsync(object viewModel, string? title = null)
    {
        Diag($"[BASE] ShowDialogAsync entered for {viewModel.GetType().Name}");
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
        {
            Diag("[BASE] Not IClassicDesktopStyleApplicationLifetime");
            return false;
        }

        if (desktop.MainWindow is null)
        {
            Diag("[BASE] MainWindow is null");
            return false;
        }

        Diag("[BASE] Building view...");
        var view = BuildView(viewModel);
        if (view is null)
        {
            throw new InvalidOperationException($"Could not locate a view for the view model '{viewModel.GetType().FullName}'.");
        }
        Diag($"[BASE] View built: {view.GetType().Name}");

        var owner = desktop.MainWindow;
        var options = GetModalDialogOptions() ?? new ModalDialogOptions();
        Diag("[BASE] Creating modal dialog session...");
        var session = _modalDialogService.Create(owner, view, title, options);
        var dialog = session.Dialog;
        Diag("[BASE] Session created OK");

        // Provide close callback for shared dialog view models so they can close themselves.
        void CloseDialog(bool result) => dialog.Close(result);

        if (viewModel is ConfirmationDialogViewModelBase confirmationVm)
        {
            confirmationVm.CloseDialog = CloseDialog;
        }
        else if (viewModel is InformationDialogViewModelBase infoVm)
        {
            infoVm.CloseDialog = CloseDialog;
        }

        view.DataContext = viewModel;

        dialog.Opened += (_, _) =>
        {
            Diag("[BASE] Dialog Opened event fired");
            Dispatcher.UIThread.Post(session.FocusFirstElement, DispatcherPriority.Background);
            dialog.KeyDown += session.KeyDownHandler;
        };

        var previousDialog = CurrentDialog;
        _dialogStack.Push(dialog);
        var focusState = OnDialogOpening(dialog, owner, previousDialog, options);

        try
        {
            Diag("[BASE] Calling dialog.ShowDialog<bool?>(owner)...");
            var result = await dialog.ShowDialog<bool?>(owner).ConfigureAwait(false);
            Diag($"[BASE] ShowDialog returned: {result}");
            return result ?? false;
        }
        finally
        {
            if (_dialogStack.Count > 0 && ReferenceEquals(_dialogStack.Peek(), dialog))
            {
                _dialogStack.Pop();
            }

            session.Dispose();
            dialog.KeyDown -= session.KeyDownHandler;

            OnDialogClosing(dialog, owner, focusState, options);
        }
    }

    /// <summary>
    /// Builds the view for the given view model using the view locator.
    /// </summary>
    protected abstract UserControl? BuildView(object viewModel);

    /// <summary>
    /// Returns modal dialog options for session creation.
    /// Override to customize layout and sizing behavior.
    /// </summary>
    protected virtual ModalDialogOptions? GetModalDialogOptions() => null;

    /// <summary>
    /// Called after dialog is pushed to stack but before showing.
    /// Handles owner/previous dialog disabling and focus capture.
    /// </summary>
    /// <param name="dialog">The dialog about to be shown.</param>
    /// <param name="owner">The main window owner.</param>
    /// <param name="previousDialog">The previously active dialog, or null if this is top-level.</param>
    /// <param name="options">Modal dialog options for the session.</param>
    /// <returns>State for restoration in OnDialogClosing.</returns>
    protected virtual DialogFocusState OnDialogOpening(Window dialog, Window owner, Window? previousDialog, ModalDialogOptions options)
    {
        var previousFocus = owner.FocusManager?.GetFocusedElement();

        if (options.FreezeOwner)
        {
            owner.IsEnabled = false;
        }

        return new DialogFocusState(previousDialog, previousFocus);
    }

    /// <summary>
    /// Called when dialog is closing. Restores owner/previous dialog state and focus.
    /// </summary>
    /// <param name="dialog">The dialog being closed.</param>
    /// <param name="owner">The main window owner.</param>
    /// <param name="focusState">Captured focus state for restoration.</param>
    /// <param name="options">Modal dialog options for the session.</param>
    protected virtual void OnDialogClosing(Window dialog, Window owner, DialogFocusState focusState, ModalDialogOptions options)
    {
        Dispatcher.UIThread.Post(() =>
        {
            if (options.FreezeOwner)
            {
                owner.IsEnabled = true;
            }
            focusState.PreviousFocus?.Focus();
        }, DispatcherPriority.Background);
    }

    /// <summary>
    /// Captures dialog focus state for restoration after closure.
    /// </summary>
    protected sealed class DialogFocusState
    {
        public Window? PreviousDialog { get; }
        public IInputElement? PreviousFocus { get; }

        public DialogFocusState(Window? previousDialog, IInputElement? previousFocus)
        {
            PreviousDialog = previousDialog;
            PreviousFocus = previousFocus;
        }
    }
}

