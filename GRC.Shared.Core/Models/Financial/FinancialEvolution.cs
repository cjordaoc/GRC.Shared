using GRC.Shared.Core.Models.Core;

namespace GRC.Shared.Core.Models.Financial;

public class FinancialEvolution
{
	public int Id { get; set; }

	public string ClosingPeriodId { get; set; } = string.Empty;

	public int EngagementId { get; set; }

	public decimal? BudgetHours { get; set; }

	public decimal? ChargedHours { get; set; }

	public decimal? FYTDHours { get; set; }

	public decimal? AdditionalHours { get; set; }

	public decimal? ValueData { get; set; }

	public decimal? RevenueToGoValue { get; set; }

	public decimal? RevenueToDateValue { get; set; }

	public decimal? BudgetMargin { get; set; }

	public decimal? ToDateMargin { get; set; }

	public decimal? FYTDMargin { get; set; }

	public decimal? ExpenseBudget { get; set; }

	public decimal? ExpensesToDate { get; set; }

	public decimal? FYTDExpenses { get; set; }

	public int? FiscalYearId { get; set; }

	public Engagement Engagement { get; set; }

	public FiscalYear? FiscalYear { get; set; }
}
