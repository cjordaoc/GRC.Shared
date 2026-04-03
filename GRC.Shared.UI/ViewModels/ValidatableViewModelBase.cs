using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GRC.Shared.UI.Messages;
using GRC.Shared.UI.Services;

namespace GRC.Shared.UI.ViewModels;

/// <summary>
/// Provides messenger-driven refresh handling and command helpers for validatable view models.
/// </summary>
public abstract partial class ValidatableViewModelBase : ObservableValidator, IRecipient<RefreshViewMessage>
{
    private readonly IReadOnlyCollection<string>? _autoRefreshTargets;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatableViewModelBase"/> class.
    /// </summary>
    /// <param name="messenger">Optional messenger instance used for cross-component communication.</param>
    /// <param name="autoRefreshTargets">Optional refresh targets that should trigger an automatic reload.</param>
    protected ValidatableViewModelBase(IMessenger? messenger = null, IEnumerable<string>? autoRefreshTargets = null)
    {
        Messenger = messenger ?? WeakReferenceMessenger.Default;
        _autoRefreshTargets = autoRefreshTargets?.Where(static target => !string.IsNullOrWhiteSpace(target)).ToArray();
        Messenger.RegisterAll(this);
    }

    /// <summary>
    /// Gets the messenger used to communicate across view models.
    /// </summary>
    protected IMessenger Messenger { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the view model is currently performing a background operation.
    /// </summary>
    [ObservableProperty]
    private bool _isBusy;

    /// <summary>
    /// Called after the <see cref="IsBusy"/> property changes.
    /// Override in derived classes to react to busy-state transitions (e.g. refresh command eligibility).
    /// </summary>
    /// <param name="value">The new busy-state value.</param>
    protected virtual void OnBusyStateChanged(bool value) { }

    /// <summary>Source-generated hook; delegates to the virtual <see cref="OnBusyStateChanged"/>.</summary>
    partial void OnIsBusyChanged(bool value) => OnBusyStateChanged(value);

    /// <summary>
    /// Loads any external data required by the view model. Override to supply custom loading logic.
    /// </summary>
    /// <returns>A task that completes when the load cycle ends.</returns>
    public virtual Task LoadDataAsync() => Task.CompletedTask;

    /// <inheritdoc />
    public virtual void Receive(RefreshViewMessage message)
    {
        if (_autoRefreshTargets is null)
        {
            return;
        }

        if (_autoRefreshTargets.Any(message.Matches))
        {
            _ = SafeRefreshAsync();
        }
    }

    /// <summary>
    /// Notifies the provided command that its execution eligibility may have changed.
    /// </summary>
    /// <param name="command">The command requiring a refresh.</param>
    protected static void NotifyCommandCanExecute(IRelayCommand? command)
    {
        if (command is null)
        {
            return;
        }

        if (Dispatcher.UIThread.CheckAccess())
        {
            command.NotifyCanExecuteChanged();
            return;
        }

        Dispatcher.UIThread.Post(command.NotifyCanExecuteChanged);
    }

    /// <summary>
    /// Executes <see cref="LoadDataAsync"/> with error handling so that messenger-driven
    /// refreshes surface failures via toast instead of silently swallowing them.
    /// </summary>
    private async Task SafeRefreshAsync()
    {
        try
        {
            await LoadDataAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ToastService.ShowError($"Refresh failed: {ex.Message}");
        }
    }
}

