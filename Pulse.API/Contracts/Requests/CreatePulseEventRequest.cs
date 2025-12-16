using Pulse.API.Domain.Events;

namespace Pulse.API.Contracts.Requests;

/// <summary>
/// Request to register a new Pulse event.
/// </summary>
public class CreatePulseEventRequest
{
    public Guid CompanyId { get; set; }
    public Guid? IdentityId { get; set; }
    public EventPlatform Platform { get; set; }
    public PulseEventType Type { get; set; }
    public DateTimeOffset OccurredAt { get; set; }
    public string Payload { get; set; } = null!;
}