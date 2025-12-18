using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Pulse.Discord.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscordClient(this IServiceCollection services)
    {
        // Discord client
        services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds
        }));

        // 🔑 InteractionService MOET via factory
        services.AddSingleton<InteractionService>(sp =>
        {
            DiscordSocketClient? client = sp.GetRequiredService<DiscordSocketClient>();

            return new InteractionService(client, new InteractionServiceConfig
            {
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Info
            });
        });

        return services;
    }
}
