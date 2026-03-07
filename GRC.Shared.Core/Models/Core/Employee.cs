using System;

namespace GRC.Shared.Core.Models.Core;

public class Employee
{
	public string Gpn { get; set; } = string.Empty;

	public string EmployeeName { get; set; } = string.Empty;

	public string Office { get; set; } = string.Empty;

	public string CostCenter { get; set; } = string.Empty;

	public DateTime StartDate { get; set; }

	public DateTime EndDate { get; set; }

	public bool IsEyEmployee { get; set; }

	public bool IsContractor { get; set; }
}
