namespace GRC.Shared.Core.Models.Core;

/// <summary>
/// Lightweight recipient assignment used within export DTOs
/// to carry email, optional name, and position (Manager / SeniorManager).
/// </summary>
public sealed record RecipientAssignment(string Email, string? Name, string Position);
