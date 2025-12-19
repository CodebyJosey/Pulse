namespace Pulse.API.Contracts.Modules;

public record GuildModuleDto(
    string Key,
    string Name,
    string Description,
    bool Enabled
);
