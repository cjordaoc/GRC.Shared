using System.Collections.Generic;
using GRC.Shared.Core.Models.Assignments;

namespace GRC.Shared.Core.Models.Core;

public class Manager
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public string WindowsLogin { get; set; } = string.Empty;

	public string EngagementManagerGui { get; set; } = string.Empty;

	public string Position { get; set; } = string.Empty;

	public ICollection<EngagementManagerAssignment> EngagementAssignments { get; set; } = new List<EngagementManagerAssignment>();
}
