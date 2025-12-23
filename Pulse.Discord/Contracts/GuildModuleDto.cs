namespace Pulse.Discord.Contracts;

public record GuildModuleDto(
    string Key,
    string Name,
    string Description,
    bool Enabled,
    DateTimeOffset UpdatedAt
);
