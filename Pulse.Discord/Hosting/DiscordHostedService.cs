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
        // 🔥 BELANGRIJK: error handler registreren
        _interactions.SlashCommandExecuted += OnSlashCommandExecutedAsync;

        string token = Environment.GetEnvironmentVariable("DISCORD_TOKEN")
            ?? throw new InvalidOperationException("DISCORD_TOKEN missing!");

        // Logging
        _client.Log += msg =>
        {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        };

        // Interaction dispatcher
        _client.InteractionCreated += async interaction =>
        {
            try
            {
                SocketInteractionContext? ctx = new SocketInteractionContext(_client, interaction);
                await _interactions.ExecuteCommandAsync(ctx, _services);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        };

        // Login & start
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // Ready event
        _client.Ready += async () =>
        {
            // Commands laden
            await _interactions.AddModulesAsync(
                typeof(Pulse.Discord.Interactions.PulseSetupCommand).Assembly,
                _services
            );

            // DEV: per guild registreren (direct zichtbaar)
            foreach (SocketGuild guild in _client.Guilds)
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

    private async Task OnSlashCommandExecutedAsync(
        SlashCommandInfo command,
        IInteractionContext context,
        IResult result)
    {
        if (result.IsSuccess)
        {
            return;
        }

        if (context.Interaction.HasResponded)
        {
            return;
        }

        string message = result.ErrorReason
            ?? "This command cannot be executed right now.";

        Embed? embed = new EmbedBuilder()
            .WithTitle("❌ Command not available")
            .WithDescription(message)
            .WithColor(Color.Red)
            .Build();

        try
        {
            await context.Interaction.RespondAsync(
                embed: embed,
                ephemeral: true
            );
        }
        catch
        {
        }
    }
}
