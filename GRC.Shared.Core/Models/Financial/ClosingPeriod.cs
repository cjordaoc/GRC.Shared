using System;
using System.Collections.Generic;
using GRC.Shared.Core.Models.Allocations;
using GRC.Shared.Core.Models.Core;

namespace GRC.Shared.Core.Models.Financial;

public class ClosingPeriod
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public int FiscalYearId { get; set; }

	public FiscalYear FiscalYear { get; set; }

	public DateTime PeriodStart { get; set; }

	public DateTime PeriodEnd { get; set; }

	public ICollection<ActualsEntry> ActualsEntries { get; } = new List<ActualsEntry>();

	public ICollection<PlannedAllocation> PlannedAllocations { get; } = new List<PlannedAllocation>();

	public ICollection<Engagement> Engagements { get; } = new List<Engagement>();

	public bool IsLocked { get; set; }
}
