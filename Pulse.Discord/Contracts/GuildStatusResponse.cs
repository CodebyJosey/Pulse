namespace Pulse.Discord.Contracts;

public record GuildStatusResponse(
    bool Claimed,
    Guid? CompanyId,
    string? CompanyName,
    DateTimeOffset? ConnectedAt
);
