using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Pulse.Discord.Services;

public sealed class ModuleSyncBackgroundService : BackgroundService
{
    private readonly ModuleStateService _modules;
    private readonly BotKeyStore _keys;
    private readonly ILogger<ModuleSyncBackgroundService> _logger;

    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(30);

    public ModuleSyncBackgroundService(
        ModuleStateService modules,
        BotKeyStore keys,
        ILogger<ModuleSyncBackgroundService> logger)
    {
        _modules = modules;
        _keys = keys;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Module sync service started.");

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (ulong guildId in _keys.GetAllGuilds())
                {
                    bool changed = await _modules.CheckForUpdatesAsync(guildId);

                    if (changed)
                    {
                        _logger.LogInformation(
                            "Modules updated for guild {GuildId}",
                            guildId
                        );
                    }
                }

                await Task.Delay(Interval, stoppingToken);
            }
        }
        catch (TaskCanceledException)
        {
            // 🔕 Verwacht bij shutdown — NIET loggen als error
            _logger.LogInformation("Module sync service stopping.");
        }
        catch (Exception ex)
        {
            // ❗ Échte fout
            _logger.LogError(ex, "Module sync service crashed.");
            throw;
        }
    }

}