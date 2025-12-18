using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Pulse.Discord.Hosting;

public class DiscordHostedService : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interactions;
    private readonly IServiceProvider _services;

    public DiscordHostedService(
        DiscordSocketClient client,
        InteractionService interactions,
        IServiceProvider services)
    {
        _client = client;
        _interactions = interactions;
        _services = services;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        string token = Environment.GetEnvironmentVariable("DISCORD_TOKEN")
            ?? throw new InvalidOperationException("DISCORD_TOKEN missing!");

        _client.Log += msg =>
        {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        };

        _client.InteractionCreated += async interaction =>
        {
            SocketInteractionContext ctx = new SocketInteractionContext(_client, interaction);
            await _interactions.ExecuteCommandAsync(ctx, _services);
        };

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        _client.Ready += async () =>
        {
            // 👇 DIT IS DE BELANGRIJKE REGEL
            await _interactions.AddModulesAsync(
                typeof(Pulse.Discord.Interactions.PulseSetupCommand).Assembly,
                _services
            );

            // 👇 DEV: registreer per guild (DIRECT zichtbaar)
            foreach (SocketGuild? guild in _client.Guilds)
            {
                await _interactions.RegisterCommandsToGuildAsync(guild.Id);
            }

            Console.WriteLine("✅ Slash commands registered");
        };
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.StopAsync();
        await _client.LogoutAsync();
    }
}