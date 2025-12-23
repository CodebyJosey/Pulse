using Discord.Interactions;
using Pulse.Discord.Guards;
using Pulse.Discord.Services;

namespace Pulse.Discord.Interactions.Fun;

[ModuleEnabledGuard("fun")]
public class PingCommand : InteractionModuleBase<SocketInteractionContext>
{
    private readonly CompanyLoggingService _logs;

    public PingCommand(CompanyLoggingService logs)
    {
        _logs = logs;
    }

    [SlashCommand("ping", "Ping the bot")]
    public async Task Ping()
    {
        await RespondAsync("🏓 Pong!");

        await _logs.LogAsync(
            guildId: Context.Guild.Id,
            moduleKey: "fun",
            eventType: "CommandExecuted",
            message: "/ping executed",
            metadata: new
            {
                userId = Context.User.Id
            }
        );
    }
}