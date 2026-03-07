using System;
using System.Collections.Generic;
using GRC.Shared.Core.Models.Financial;

namespace GRC.Shared.Core.Models.Core;

public class EtcExport
{
	public string EngagementId { get; set; } = string.Empty;

	public string EngagementName { get; set; } = string.Empty;

	public string Customer { get; set; } = string.Empty;

	public DateTime? LastEtcDate { get; set; }

	public DateTime? ProposedNextEtcDate { get; set; }

	public IReadOnlyList<Recipient> Recipients { get; set; } = new List<Recipient>();

	public IReadOnlyList<FiscalYear> FiscalYears { get; set; } = new List<FiscalYear>();
}
