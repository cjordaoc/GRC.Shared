using System;
using GRC.Shared.Core.Models.Financial;

namespace GRC.Shared.Core.Models;

public sealed class FiscalYearCloseResult
{
	public FiscalYear ClosedFiscalYear { get; }

	public FiscalYear? PromotedFiscalYear { get; }

	public FiscalYearCloseResult(FiscalYear closedFiscalYear, FiscalYear? promotedFiscalYear)
	{
		ClosedFiscalYear = closedFiscalYear ?? throw new ArgumentNullException("closedFiscalYear");
		PromotedFiscalYear = promotedFiscalYear;
	}
}
