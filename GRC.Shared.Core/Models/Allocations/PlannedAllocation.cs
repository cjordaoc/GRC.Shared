using GRC.Shared.Core.Models.Core;
using GRC.Shared.Core.Models.Financial;

namespace GRC.Shared.Core.Models.Allocations;

public class PlannedAllocation
{
	public int Id { get; set; }

	public int EngagementId { get; set; }

	public Engagement Engagement { get; set; }

	public int ClosingPeriodId { get; set; }

	public ClosingPeriod ClosingPeriod { get; set; }

	public decimal AllocatedHours { get; set; }
}
