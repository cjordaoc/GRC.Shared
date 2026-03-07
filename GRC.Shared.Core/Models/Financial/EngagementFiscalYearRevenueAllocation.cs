using System;
using GRC.Shared.Core.Models.Core;

namespace GRC.Shared.Core.Models.Financial;

public class EngagementFiscalYearRevenueAllocation
{
	public int Id { get; set; }

	public int EngagementId { get; set; }

	public Engagement Engagement { get; set; }

	public int FiscalYearId { get; set; }

	public FiscalYear FiscalYear { get; set; }

	public int ClosingPeriodId { get; set; }

	public ClosingPeriod ClosingPeriod { get; set; }

	public decimal ToGoValue { get; set; }

	public decimal ToDateValue { get; set; }

	public decimal TotalValue => ToGoValue + ToDateValue;

	public DateTime? LastUpdateDate { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public EngagementFiscalYearRevenueAllocation()
	{
		CreatedAt = DateTime.UtcNow;
		UpdatedAt = DateTime.UtcNow;
	}
}
