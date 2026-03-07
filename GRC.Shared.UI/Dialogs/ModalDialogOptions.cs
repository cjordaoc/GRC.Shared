using Avalonia;

namespace GRC.Shared.UI.Dialogs;

public enum ModalDialogLayout
{
    CenteredOverlay,
    OwnerAligned
}

public sealed class ModalDialogOptions
{
    public ModalDialogLayout Layout { get; init; } = ModalDialogLayout.CenteredOverlay;

    public double ContentSizeRatio { get; init; } = 0.85d;

    public Thickness? ContainerMargin { get; init; }

    /// <summary>
    /// When true, the owner window is disabled while the dialog is open.
    /// </summary>
    public bool FreezeOwner { get; init; }

    /// <summary>
    /// Resource key for the dialog content size ratio (e.g. "DialogContentRatioStandard").
    /// </summary>
    public string? SizeRatioResourceKey { get; init; }

    /// <summary>
    /// Whether to show system window decorations (close/minimize/maximize).
    /// </summary>
    public bool ShowWindowControls { get; init; }

    /// <summary>
    /// Whether to dim the background behind the modal dialog.
    /// </summary>
    public bool DimBackground { get; init; }
}
