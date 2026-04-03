using System;
using System.Collections.Generic;

namespace GRC.Shared.Core.Models.Core;

/// <summary>
/// Fiscal-year hours breakdown used in ETC export DTOs.
/// </summary>
public class EtcFiscalYear
{
	public string Name { get; set; } = string.Empty;

	public DateTime? StartDate { get; set; }

	public IReadOnlyList<EtcRankHours> Ranks { get; set; } = Array.Empty<EtcRankHours>();
}
