using System;
using System.Collections.Generic;
using GRC.Shared.Core.Models.Assignments;
using GRC.Shared.Core.Models.Financial;
using GRC.Shared.Core.Models.Lookups;

namespace GRC.Shared.Core.Models.Core;

public class Engagement
{
	public int Id { get; set; }

	public string EngagementId { get; set; } = string.Empty;

	public string EngagementTitle { get; set; } = string.Empty;

	public string? EngagementPartner { get; set; }

	public string? EngagementManager { get; set; }

	public decimal? OpeningMargin { get; set; }

	public decimal? OpeningValue { get; set; }

	public bool IsActive { get; set; }

	public EngagementSource Source { get; set; }

	public int? CustomerId { get; set; }

	public Customer? Customer { get; set; }

	public string? Currency { get; set; }

	public string? Description { get; set; }

	public decimal? MarginPctBudget { get; set; }

	public decimal? MarginPctEtcp { get; set; }

	public string? StatusText { get; set; }

	public ClosingPeriod? ClosingPeriod { get; set; }

	public decimal? AdditionalSales { get; set; }

	public decimal? EstimatedToCompleteHours { get; set; }

	public decimal? ExpensesEtcp { get; set; }

	public decimal? InitialHoursBudget { get; set; }

	public decimal? OpeningExpenses { get; set; }

	public DateTime? ProposedNextEtcDate { get; set; }

	public EngagementStatus Status { get; set; }

	public decimal? ValueEtcp { get; set; }

	public decimal? ValueToAllocate { get; set; }

	public int? LastClosingPeriodId { get; set; }

	public ClosingPeriod? LastClosingPeriod { get; set; }

	public DateTime? LastEtcDate { get; set; }

	public int? UnbilledRevenueDays { get; set; }

	public string? LastClosingPeriodName { get; set; }

	public ICollection<FinancialEvolution> FinancialEvolutions { get; set; } = new List<FinancialEvolution>();

	public ICollection<EngagementPapd> EngagementPapds { get; set; } = new List<EngagementPapd>();

	public ICollection<EngagementManagerAssignment> ManagerAssignments { get; set; } = new List<EngagementManagerAssignment>();

	public ICollection<EngagementRankBudget> RankBudgets { get; set; } = new List<EngagementRankBudget>();

	public ICollection<EngagementFiscalYearRevenueAllocation> RevenueAllocations { get; set; } = new List<EngagementFiscalYearRevenueAllocation>();

	public ICollection<EngagementAdditionalSale> AdditionalSaleItems { get; set; } = new List<EngagementAdditionalSale>();
}
