using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Pulse.Discord.Client;
using Pulse.Discord.Services;

namespace Pulse.Discord.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscordClient(this IServiceCollection services)
    {
        services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds
        }));

        services.AddSingleton<InteractionService>(sp =>
        {
            DiscordSocketClient client = sp.GetRequiredService<DiscordSocketClient>();

            return new InteractionService(client, new InteractionServiceConfig
            {
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Info
            });
        });

        return services;
    }

    public static IServiceCollection AddPulseServices(
        this IServiceCollection services,
        string apiBaseUrl)
    {
        services.AddHttpClient<PulseApiClient>(http =>
        {
            http.BaseAddress = new Uri(apiBaseUrl);
        });

        services.AddSingleton<BotKeyStore>();
        services.AddSingleton<ModuleStateService>();
        services.AddHostedService<ModuleSyncBackgroundService>();

        return services;
    }
}
