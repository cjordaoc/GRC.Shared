namespace GRC.Shared.Core.Models.Core;

public class ConnectionTestResult
{
	public bool IsSuccessful { get; set; }

	public string? ErrorMessage { get; set; }

	public ConnectionTestResult()
	{
	}

	public ConnectionTestResult(bool isSuccessful, string? errorMessage = null)
	{
		IsSuccessful = isSuccessful;
		ErrorMessage = errorMessage;
	}
}
