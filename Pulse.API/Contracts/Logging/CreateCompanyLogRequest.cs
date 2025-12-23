namespace Pulse.API.Contracts.Logging;

public record CreateCompanyLogRequest(
    string GuildId,
    string ModuleKey,
    string EventType,
    string Message,
    string? MetadataJson
);