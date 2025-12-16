using Pulse.API.Domain.Companies;
using Pulse.API.Domain.Identities;

namespace Pulse.API.Domain.Events;

/// <summary>
/// Represents a single immutable event that occurred within a company.
/// Events are append-only and form the historical truth of Pulse.
/// </summary>
public class PulseEvent
{
    /// <summary>
    /// Unique identifier of the event.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The company this event belongs to.
    /// </summary>
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    /// <summary>
    /// The identity associated with the event.
    /// </summary>
    public Guid IdentityId { get; set; }
    public Identity? Identity { get; set; }

    /// <summary>
    /// The platform that produced this event (e.g. Minecraft, Discord).
    /// </summary>
    public EventPlatform Platform { get; set; }

    /// <summary>
    /// The type of event that occured.
    /// </summary>
    public PulseEventType Type { get; set; }

    /// <summary>
    /// The moment the event occured (in UTC)
    /// </summary>
    public DateTimeOffset OccurredAt { get; set; }

    /// <summary>
    /// Event-specific data stored as JSON.
    /// </summary>
    public string Payload { get; set; } = null!;
}