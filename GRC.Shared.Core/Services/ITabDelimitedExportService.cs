using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRC.Shared.Core.Services;

/// <summary>
/// Exports tabular data as tab-delimited text files.
/// </summary>
public interface ITabDelimitedExportService
{
    /// <summary>
    /// Writes header and detail rows to a tab-delimited file at the specified path.
    /// </summary>
    Task ExportAsync(string filePath, IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rows);
}
