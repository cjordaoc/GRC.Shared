namespace GRC.Shared.Core.Models.Core;

public class Recipient
{
	public string Name { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public RoleOption DominantRole { get; set; }
}
