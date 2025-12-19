namespace Pulse.API.Contracts.Bot;

public record GuildStatusResponse(
    bool Claimed,
    Guid? CompanyId,
    string? CompanyName,
    DateTimeOffset? ConnectedAt
);