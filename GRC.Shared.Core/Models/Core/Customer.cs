using System.Collections.Generic;

namespace GRC.Shared.Core.Models.Core;

public class Customer
{
	public int Id { get; set; }

	public string CustomerCode { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	public ICollection<Engagement> Engagements { get; set; } = new List<Engagement>();
}
