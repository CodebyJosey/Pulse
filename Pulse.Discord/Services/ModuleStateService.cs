using System.Reflection;
using Pulse.Discord.Client;
using Pulse.Discord.Contracts;

namespace Pulse.Discord.Services;

public sealed class ModuleStateService
{
    private readonly PulseApiClient _api;
    private readonly BotKeyStore _keys;


    private readonly Dictionary<ulong, Dictionary<string, bool>> _cache = new Dictionary<ulong, Dictionary<string, bool>>();
    private readonly Dictionary<ulong, DateTimeOffset> _lastSeen = new Dictionary<ulong, DateTimeOffset>();

    public ModuleStateService(PulseApiClient api, BotKeyStore keys)
    {
        _api = api;
        _keys = keys;
    }

    public void Invalidate(ulong guildId) => _cache.Remove(guildId);

    public async Task<bool> IsEnabledAsync(ulong guildId, string moduleKey)
    {
        if (!_cache.TryGetValue(guildId, out Dictionary<string, bool>? states))
        {
            await RefreshAsync(guildId);
            _cache.TryGetValue(guildId, out states);
        }

        return states is not null &&
            states.TryGetValue(moduleKey, out bool enabled) && enabled;
    }

    public async Task<bool> CheckForUpdatesAsync(ulong guildId)
    {
        string? apiKey = _keys.Get(guildId);

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return false;
        }

        List<GuildModuleDto> modules;

        try
        {
            modules = await _api.GetAsync<List<GuildModuleDto>>(
                $"api/guilds/{guildId}/modules",
                apiKey
            );
        }
        catch
        {
            return false;
        }

        if (modules.Count == 0) return false;

        DateTimeOffset newest = modules.Max(module => module.UpdatedAt);

        if (_lastSeen.TryGetValue(guildId, out DateTimeOffset last) && newest <= last)
        {
            return false;
        }

        _lastSeen[guildId] = newest;
        _cache[guildId] = modules.ToDictionary(
            module => module.Key,
            module => module.Enabled,
            StringComparer.OrdinalIgnoreCase
        );

        return true;
    }

    private async Task RefreshAsync(ulong guildId)
    {
        await CheckForUpdatesAsync(guildId);
    }
}