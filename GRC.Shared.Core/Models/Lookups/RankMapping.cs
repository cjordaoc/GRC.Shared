using System;

namespace GRC.Shared.Core.Models.Lookups;

public class RankMapping
{
	public int Id { get; set; }

	public string RawRank { get; set; } = string.Empty;

	public string NormalizedRank { get; set; } = string.Empty;

	public string? SpreadsheetRank { get; set; }

	public bool IsActive { get; set; }

	public DateTime? LastSeenAt { get; set; }

	public DateTime CreatedAt { get; set; }
}
