using Pulse.Discord.Client;
using Pulse.Discord.Contracts;

namespace Pulse.Discord.Services;

public sealed class ModuleStateService
{
    private readonly PulseApiClient _api;
    private readonly BotKeyStore _keys;

    private readonly Dictionary<ulong, Dictionary<string, bool>> _cache = new();
    private readonly Dictionary<ulong, DateTimeOffset> _lastSeen = new();

    public ModuleStateService(PulseApiClient api, BotKeyStore keys)
    {
        _api = api;
        _keys = keys;
    }

    /// <summary>
    /// Forceert het legen van de cache voor een guild.
    /// Wordt gebruikt na admin-updates.
    /// </summary>
    public void Invalidate(ulong guildId)
    {
        _cache.Remove(guildId);
        _lastSeen.Remove(guildId);
    }

    /// <summary>
    /// Checkt of een module actief is voor een guild.
    /// Doet automatisch één refresh als de cache leeg is.
    /// </summary>
    public async Task<bool> IsEnabledAsync(ulong guildId, string moduleKey)
    {
        if (!_cache.TryGetValue(guildId, out Dictionary<string, bool>? states))
        {
            await RefreshAsync(guildId);
            _cache.TryGetValue(guildId, out states);
        }

        return states is not null &&
               states.TryGetValue(moduleKey, out bool enabled) &&
               enabled;
    }

    /// <summary>
    /// Wordt gebruikt door background services om te checken
    /// of er updates zijn sinds de laatste fetch.
    /// </summary>
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

        if (modules.Count == 0)
            return false;

        DateTimeOffset newest = modules.Max(m => m.UpdatedAt);

        if (_lastSeen.TryGetValue(guildId, out DateTimeOffset last) && newest <= last)
        {
            return false;
        }

        _lastSeen[guildId] = newest;

        _cache[guildId] = modules.ToDictionary(
            m => m.Key,
            m => m.Enabled,
            StringComparer.OrdinalIgnoreCase
        );

        return true;
    }

    /// <summary>
    /// Interne refresh-logica.
    /// Wordt aangeroepen door IsEnabledAsync bij lege cache.
    /// </summary>
    private async Task RefreshAsync(ulong guildId)
    {
        await CheckForUpdatesAsync(guildId);
    }
}
