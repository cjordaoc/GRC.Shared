using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GRC.Shared.UI.ViewModels.Dialogs;

/// <summary>
/// Base view model for confirmation dialogs with Confirm/Cancel actions.
/// </summary>
public abstract partial class ConfirmationDialogViewModelBase : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private string _confirmButtonText = "Confirm";

    [ObservableProperty]
    private string _cancelButtonText = "Cancel";

    /// <summary>
    /// Optional callback invoked when the user confirms.
    /// </summary>
    public Action? OnConfirmed { get; set; }

    /// <summary>
    /// Optional callback invoked when the user cancels.
    /// </summary>
    public Action? OnCanceled { get; set; }

    /// <summary>
    /// Optional callback to close the hosting dialog with a boolean result.
    /// </summary>
    public Action<bool>? CloseDialog { get; set; }

    /// <summary>
    /// Gets the relay command bound to the confirm button.
    /// </summary>
    public IRelayCommand ConfirmCommand => _confirmCommand ??= new RelayCommand(ExecuteConfirm);
    private IRelayCommand? _confirmCommand;

    /// <summary>
    /// Gets the relay command bound to the cancel button.
    /// </summary>
    public IRelayCommand CancelCommand => _cancelCommand ??= new RelayCommand(ExecuteCancel);
    private IRelayCommand? _cancelCommand;

    private void ExecuteConfirm()
    {
        OnConfirmed?.Invoke();
        CloseDialog?.Invoke(true);
    }

    private void ExecuteCancel()
    {
        OnCanceled?.Invoke();
        CloseDialog?.Invoke(false);
    }
}
