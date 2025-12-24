using Pulse.Discord.Client;
using Pulse.Discord.Contracts;

namespace Pulse.Discord.Services;

public sealed class GuildLoggingSettingsService
{
    private readonly PulseApiClient _api;
    private readonly BotKeyStore _keys;

    private readonly Dictionary<ulong, ulong?> _cache = new();

    public GuildLoggingSettingsService(PulseApiClient api, BotKeyStore keys)
    {
        _api = api;
        _keys = keys;
    }

    public async Task<ulong?> GetLogChannelAsync(ulong guildId)
    {
        if (_cache.TryGetValue(guildId, out ulong? channel))
            return channel;

        string? apiKey = _keys.Get(guildId);
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _cache[guildId] = null;
            return null;
        }

        try
        {
            GuildLoggingSettingsDto dto =
                await _api.GetAsync<GuildLoggingSettingsDto>(
                    // 🔥 JUISTE BOT-ENDPOINT
                    $"api/bot/guilds/{guildId}/logging/channel",
                    apiKey
                );

            _cache[guildId] = dto.LogChannelId;
            return dto.LogChannelId;
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"[GuildLoggingSettings] Failed to load log channel for {guildId}: {ex.Message}"
            );

            // ✅ cache ook null → geen silent retry-loop
            _cache[guildId] = null;
            return null;
        }
    }

    public void Invalidate(ulong guildId)
    {
        _cache.Remove(guildId);
    }
}
