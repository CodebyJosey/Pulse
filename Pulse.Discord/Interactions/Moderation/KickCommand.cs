using Discord.Interactions;
using Pulse.Discord.Guards;

namespace Pulse.Discord.Interactions.Moderation;

[ModuleEnabledGuard("moderation")]
public class KickCommand : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("kick", "Kick a user")]
    public async Task Kick()
    {

    }
}
