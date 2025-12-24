namespace Pulse.Discord.Contracts;

public record GuildLoggingSettingsDto(
    ulong? LogChannelId,
    DateTimeOffset UpdatedAt
);
