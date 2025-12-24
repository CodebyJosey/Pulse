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

        // 1️⃣ Modules LADEN vóórdat Discord events binnenkomen
        await _interactions.AddModulesAsync(
            typeof(DiscordHostedService).Assembly,
            _services
        );

        // 2️⃣ Events registreren
        _client.InteractionCreated += async interaction =>
        {
            SocketInteractionContext ctx = new SocketInteractionContext(_client, interaction);
            await _interactions.ExecuteCommandAsync(ctx, _services);
        };

        _interactions.SlashCommandExecuted += OnSlashCommandExecutedAsync;

        // 3️⃣ Login & start
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // 4️⃣ Commands registreren bij Discord (NA Ready)
        _client.Ready += async () =>
        {
            foreach (SocketGuild guild in _client.Guilds)
            {
                await _interactions.RegisterCommandsToGuildAsync(
                    guild.Id,
                    deleteMissing: true
                );
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
