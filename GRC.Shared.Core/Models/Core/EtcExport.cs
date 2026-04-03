using System;
using System.Collections.Generic;

namespace GRC.Shared.Core.Models.Core;

public class EtcExport
{
	public string EngagementId { get; set; } = string.Empty;

	public string EngagementName { get; set; } = string.Empty;

	public string Customer { get; set; } = string.Empty;

	public DateTime? LastEtcDate { get; set; }

	public DateTime? ProposedNextEtcDate { get; set; }

	public IReadOnlyList<RecipientAssignment> Recipients { get; set; } = Array.Empty<RecipientAssignment>();

	public IReadOnlyList<EtcFiscalYear> FiscalYears { get; set; } = Array.Empty<EtcFiscalYear>();
}
