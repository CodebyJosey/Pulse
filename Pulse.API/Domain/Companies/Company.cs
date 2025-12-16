namespace Pulse.API.Domain.Companies;

/// <summary>
/// Represents a game network or community within Pulse.
/// </summary>
public class Company
{
    /// <summary>
    /// Unique identifier of the company.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Display name of the company.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// UCT timestamp when the company was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}