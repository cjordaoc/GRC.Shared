using System.Collections.Generic;
using GRC.Shared.Core.Models.Assignments;

namespace GRC.Shared.Core.Models.Core;

public class Papd
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Level { get; set; } = string.Empty;

	public string WindowsLogin { get; set; } = string.Empty;

	public string EngagementPapdGui { get; set; } = string.Empty;

	public ICollection<EngagementPapd> EngagementPapds { get; set; } = new List<EngagementPapd>();
}
