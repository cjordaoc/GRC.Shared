using System.ComponentModel.DataAnnotations;

namespace GRC.Shared.Core.Models.Core;

public class Setting
{
	[Key]
	public string Key { get; set; } = string.Empty;

	public string Value { get; set; } = string.Empty;
}
