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
        // Discord socket client
        services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents =
                GatewayIntents.Guilds |
                GatewayIntents.GuildMessages
        }));

        // Interaction service
        services.AddSingleton<InteractionService>(sp =>
        {
            DiscordSocketClient client = sp.GetRequiredService<DiscordSocketClient>();

            return new InteractionService(client, new InteractionServiceConfig
            {
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Info
            });
        });

        // 🔥 REGISTREER DE REST CLIENT DIE BIJ DE SOCKET HOORT
        services.AddSingleton(sp =>
        {
            DiscordSocketClient socket = sp.GetRequiredService<DiscordSocketClient>();
            return socket.Rest; // DiscordSocketRestClient
        });

        return services;
    }

    public static IServiceCollection AddPulseServices(
        this IServiceCollection services,
        string apiBaseUrl)
    {
        // API client
        services.AddHttpClient<PulseApiClient>(http =>
        {
            http.BaseAddress = new Uri(apiBaseUrl);
        });

        // Bot state
        services.AddSingleton<BotKeyStore>();

        // Module & logging services
        services.AddSingleton<ModuleStateService>();
        services.AddSingleton<GuildLoggingSettingsService>();
        services.AddSingleton<DiscordLogChannelService>();
        services.AddSingleton<CompanyLoggingService>();

        // Background sync
        services.AddHostedService<ModuleSyncBackgroundService>();

        return services;
    }
}
