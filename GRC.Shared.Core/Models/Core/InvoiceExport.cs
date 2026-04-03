using System;
using System.Collections.Generic;

namespace GRC.Shared.Core.Models.Core;

public class InvoiceExport
{
	public int InvoiceItemId { get; set; }

	public string EngagementId { get; set; } = string.Empty;

	public string Customer { get; set; } = string.Empty;

	public string? FocalPointName { get; set; }

	public string? FocalPointEmail { get; set; }

	public string? Cnpj { get; set; }

	public string? Po { get; set; }

	public string? Frs { get; set; }

	public string? CustomerTicket { get; set; }

	public string PaymentTypeCode { get; set; } = string.Empty;

	public string PaymentTypeName { get; set; } = string.Empty;

	public decimal InvoiceValue { get; set; }

	public DateTime DateOfEmission { get; set; }

	public DateTime InvoiceDueDate { get; set; }

	public string? EmailsToSend { get; set; }

	public IReadOnlyList<RecipientAssignment> Recipients { get; set; } = Array.Empty<RecipientAssignment>();

	public string InvoiceDescription { get; set; } = string.Empty;

	public string? AdditionalNotes { get; set; }
}
