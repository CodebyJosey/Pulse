namespace Pulse.API.Domain.Events;

/// <summary>
/// Represents the type of event that occured within Pulse.
/// </summary>
public enum PulseEventType
{
    PlayerJoin = 1,
    PlayerLeave = 2,
    CommandExecuted = 3,
    ServerHeartbeat = 4,
    MessageSent = 5
}