using System.Collections.Generic;
using System.Linq;

namespace GRC.Shared.Core.Models.Core;

public class MessageBucket
{
	public List<InvoiceExport> Invoices { get; set; } = new List<InvoiceExport>();

	public List<EtcExport> Etcs { get; set; } = new List<EtcExport>();

	public IReadOnlyList<Recipient> GetOrderedRecipients()
	{
		return (from r in Invoices.SelectMany((InvoiceExport i) => i.Recipients).Concat(Etcs.SelectMany((EtcExport e) => e.Recipients))
			group r by r.Email into g
			select g.First() into r
			orderby r.Name
			select r).ToList();
	}

	public IReadOnlyList<InvoiceExport> GetOrderedInvoices()
	{
		return Invoices.OrderBy((InvoiceExport i) => i.InvoiceItemId).ToList();
	}

	public IReadOnlyList<EtcExport> GetOrderedEtcs()
	{
		return Etcs.OrderBy((EtcExport e) => e.EngagementId).ToList();
	}
}
