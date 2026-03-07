using System;
using GRC.Shared.Core.Models.Core;
using GRC.Shared.Core.Models.Financial;

namespace GRC.Shared.Core.Models.Assignments;

public class EngagementRankBudget
{
	public long Id { get; set; }

	public int EngagementId { get; set; }

	public Engagement? Engagement { get; set; }

	public int FiscalYearId { get; set; }

	public FiscalYear? FiscalYear { get; set; }

	public int ClosingPeriodId { get; set; }

	public ClosingPeriod? ClosingPeriod { get; set; }

	public string RankName { get; set; } = string.Empty;

	public decimal BudgetHours { get; set; }

	public decimal ConsumedHours { get; set; }

	public decimal AdditionalHours { get; set; }

	public decimal RemainingHours { get; set; }

	public string Status { get; set; } = string.Empty;

	public DateTime CreatedAtUtc { get; set; }

	public DateTime? UpdatedAtUtc { get; set; }
}
