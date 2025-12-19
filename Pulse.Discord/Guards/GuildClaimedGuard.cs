using Discord;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;
using Pulse.Discord.Client;
using Pulse.Discord.Contracts;

namespace Pulse.Discord.Guards;

public class GuildClaimedGuard : PreconditionAttribute
{
    public override async Task<PreconditionResult> CheckRequirementsAsync(
        IInteractionContext context,
        ICommandInfo command,
        IServiceProvider services
    )
    {
        if (context.Guild is null)
        {
            return PreconditionResult.FromError("Deze command werkt alleen in servers.");
        }

        if (command.Name is "pulse-setup" or "pulse-status")
        {
            return PreconditionResult.FromSuccess();
        }

        PulseApiClient? api = services.GetRequiredService<PulseApiClient>();

        GuildStatusResponse? status = await api.GetAsync<GuildStatusResponse>(
            $"api/bot/guilds/{context.Guild.Id}/status"
        );

        if (!status.Claimed)
        {
            return PreconditionResult.FromError(
                "❌ Deze server is nog niet gekoppeld aan Pulse.\nGebruik `/pulse setup`."
            );
        }

        return PreconditionResult.FromSuccess();
    }
}