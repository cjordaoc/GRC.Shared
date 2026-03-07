using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GRC.Shared.Core.Models.Core;

namespace GRC.Shared.Core.Models.Assignments;

public class EngagementManagerAssignment
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public int EngagementId { get; set; }

	public Engagement Engagement { get; set; }

	public int ManagerId { get; set; }

	public Manager Manager { get; set; }
}
