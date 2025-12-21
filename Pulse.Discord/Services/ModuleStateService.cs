using System.Reflection;
using Pulse.Discord.Client;
using Pulse.Discord.Contracts;

namespace Pulse.Discord.Services;

public sealed class ModuleStateService
{
    private readonly PulseApiClient _api;
    private readonly BotKeyStore _keys;

    private readonly Dictionary<ulong, (DateTimeOffset fetchedAt, Dictionary<string, bool> states)> _cache = new();
    private readonly TimeSpan _ttl = TimeSpan.FromMinutes(2);

    public ModuleStateService(PulseApiClient api, BotKeyStore keys)
    {
        _api = api;
        _keys = keys;
    }

    public void Invalidate(ulong guildId) => _cache.Remove(guildId);

    public async Task<bool> IsEnabledAsync(ulong guildId, string moduleKey)
    {
        var states = await GetStatesAsync(guildId);
        return states.TryGetValue(moduleKey, out bool enabled) && enabled;
    }

    public async Task<Dictionary<string, bool>> GetStatesAsync(ulong guildId)
    {
        if (_cache.TryGetValue(guildId, out (DateTimeOffset fetchedAt, Dictionary<string, bool> states) entry))
        {
            if (DateTimeOffset.UtcNow - entry.fetchedAt < _ttl)
            {
                return entry.states;
            }
        }

        string? apiKey = _keys.Get(guildId);
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
        }

        List<GuildModuleDto> modules = await _api.GetAsync<List<GuildModuleDto>>(
            $"api/guilds/{guildId}/modules",
            apiKey
        );

        Dictionary<string, bool>? dict = modules.ToDictionary(module => module.Key, module => module.Enabled, StringComparer.OrdinalIgnoreCase);
        _cache[guildId] = (DateTimeOffset.UtcNow, dict);

        return dict;
    }
}