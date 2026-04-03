namespace GRC.Shared.Core.Models.Core;

/// <summary>
/// Consumed and remaining hours for a single rank within an ETC fiscal year.
/// </summary>
public class EtcRankHours
{
	public string Rank { get; set; } = string.Empty;

	public decimal ConsumedHours { get; set; }

	public decimal RemainingHours { get; set; }
}
