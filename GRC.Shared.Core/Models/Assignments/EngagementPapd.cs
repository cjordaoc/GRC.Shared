using GRC.Shared.Core.Models.Core;

namespace GRC.Shared.Core.Models.Assignments;

public class EngagementPapd
{
	public int Id { get; set; }

	public int EngagementId { get; set; }

	public Engagement Engagement { get; set; }

	public int PapdId { get; set; }

	public Papd Papd { get; set; }
}
