using Discord.Interactions;
using Pulse.Discord.Guards;

namespace Pulse.Discord.Interactions;

[GuildClaimedGuard]
public abstract class PulseModuleBase : InteractionModuleBase<SocketInteractionContext>
{
}