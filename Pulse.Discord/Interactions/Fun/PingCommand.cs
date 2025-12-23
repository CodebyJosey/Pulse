using Discord.Interactions;
using Pulse.Discord.Guards;

namespace Pulse.Discord.Interactions.Fun;

[ModuleEnabledGuard("fun")]
public class PingCommand : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "Ping the bot")]
    public async Task Ping()
    {
        await RespondAsync("🏓 Pong!");
    }
}