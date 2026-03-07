using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace GRC.Shared.UI.Services;

/// <summary>
/// Cross-platform file picker abstraction over Avalonia's IStorageProvider.
/// </summary>
public sealed class FilePickerService
{
    private readonly Window _owner;

    public FilePickerService(Window owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
    }

    /// <summary>
    /// Shows an open-file dialog and returns the selected path, or <c>null</c> if cancelled.
    /// </summary>
    public async Task<string?> OpenFileAsync(
        string? title = null,
        string? defaultExtension = null,
        IEnumerable<string>? allowedPatterns = null)
    {
        var storageProvider = _owner.StorageProvider;
        var options = new FilePickerOpenOptions
        {
            Title = title ?? "Open File",
            AllowMultiple = false,
            FileTypeFilter = BuildFileTypeFilter(defaultExtension, allowedPatterns)
        };

        var result = await storageProvider.OpenFilePickerAsync(options).ConfigureAwait(false);
        return result.FirstOrDefault()?.TryGetLocalPath();
    }

    /// <summary>
    /// Shows a save-file dialog and returns the selected path, or <c>null</c> if cancelled.
    /// </summary>
    public async Task<string?> SaveFileAsync(
        string defaultFileName,
        string? title = null,
        string? defaultExtension = null,
        IEnumerable<string>? allowedPatterns = null)
    {
        var storageProvider = _owner.StorageProvider;
        var options = new FilePickerSaveOptions
        {
            Title = title ?? "Save File",
            SuggestedFileName = defaultFileName,
            DefaultExtension = defaultExtension?.TrimStart('.'),
            FileTypeChoices = BuildFileTypeFilter(defaultExtension, allowedPatterns)
        };

        var result = await storageProvider.SaveFilePickerAsync(options).ConfigureAwait(false);
        return result?.TryGetLocalPath();
    }

    private static IReadOnlyList<FilePickerFileType>? BuildFileTypeFilter(
        string? defaultExtension,
        IEnumerable<string>? allowedPatterns)
    {
        var patterns = allowedPatterns?.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

        if ((patterns is null || patterns.Length == 0) && string.IsNullOrWhiteSpace(defaultExtension))
        {
            return null;
        }

        var effectivePatterns = patterns is { Length: > 0 }
            ? patterns
            : new[] { $"*{defaultExtension}" };

        return new[]
        {
            new FilePickerFileType("Supported Files") { Patterns = effectivePatterns }
        };
    }
}
