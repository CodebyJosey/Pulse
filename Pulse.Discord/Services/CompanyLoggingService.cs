using Discord;
using Pulse.Discord.Client;
using Pulse.Discord.Services;

public sealed class CompanyLoggingService
{
    private readonly ModuleStateService _modules;
    private readonly PulseApiClient _api;
    private readonly BotKeyStore _keys;
    private readonly DiscordLogChannelService _discordLogs;

    public CompanyLoggingService(
        ModuleStateService modules,
        PulseApiClient api,
        BotKeyStore keys,
        DiscordLogChannelService discordLogs)
    {
        _modules = modules;
        _api = api;
        _keys = keys;
        _discordLogs = discordLogs;
    }

    public async Task LogAsync(
        ulong guildId,
        string moduleKey,
        string eventType,
        string message,
        object? metadata = null)
    {
        await _modules.CheckForUpdatesAsync(guildId);
        bool enabled = await _modules.IsEnabledAsync(guildId, "logging");

        if (!enabled)
        {
            return;
        }

        string? apiKey = _keys.Get(guildId);
        if (apiKey is not null)
        {
            try
            {
                await _api.PostAsync("api/company/logs", new
                {
                    GuildId = guildId.ToString(),
                    ModuleKey = moduleKey,
                    EventType = eventType,
                    Message = message,
                    MetadataJson = metadata is null
                        ? null
                        : System.Text.Json.JsonSerializer.Serialize(metadata)
                }, apiKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CompanyLogging] DB post failed: {ex}");
            }

        }

        Embed? embed = new EmbedBuilder()
            .WithTitle($"📋 {moduleKey} log")
            .WithDescription(message)
            .WithColor(Color.DarkGrey)
            .WithTimestamp(DateTimeOffset.UtcNow)
            .Build();

        await _discordLogs.TryLogAsync(guildId, embed);
    }
}
