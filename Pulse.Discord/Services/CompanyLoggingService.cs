using System.Text.Json;
using Pulse.Discord.Client;
using Pulse.Discord.Services;

namespace Pulse.Discord.Services;

public sealed class CompanyLoggingService
{
    private readonly ModuleStateService _modules;
    private readonly PulseApiClient _api;
    private readonly BotKeyStore _keys;

    public CompanyLoggingService(
        ModuleStateService modules,
        PulseApiClient api,
        BotKeyStore keys)
    {
        _modules = modules;
        _api = api;
        _keys = keys;
    }

    public async Task LogAsync(
        ulong guildId,
        string moduleKey,
        string eventType,
        string message,
        object? metadata = null)
    {
        if (!await _modules.IsEnabledAsync(guildId, "logging"))
        {
            return;
        }

        string? apiKey = _keys.Get(guildId);
        if (apiKey is null)
        {
            return;
        }

        var payload = new
        {
            GuildId = guildId.ToString(),
            ModuleKey = moduleKey,
            EventType = eventType,
            Message = message,
            MetadataJson = metadata is null
                ? null
                : JsonSerializer.Serialize(metadata)
        };

        try
        {
            await _api.PostAsync("api/company/logs", payload, apiKey);
        }
        catch
        {
            // logging mag NOOIT bot-gedrag breken
        }
    }
}
