using Discord.Interactions;
using Pulse.Discord.Client;
using Pulse.Discord.Contracts;
using Pulse.Discord.UI.Embeds;

namespace Pulse.Discord.Interactions;

public class PulseStatusCommand : InteractionModuleBase<SocketInteractionContext>
{
    private readonly PulseApiClient _api;

    public PulseStatusCommand(PulseApiClient api)
    {
        _api = api;
    }

    [SlashCommand("pulse-status", "Check the Pulse status of this server.")]
    public async Task Status()
    {
        await DeferAsync(ephemeral: false);

        GuildStatusResponse? status = await _api.GetAsync<GuildStatusResponse>(
            $"api/bot/guilds/{Context.Guild.Id}/status"
        );

        if (!status.Claimed)
        {
            await FollowupAsync(
                embed: PulseEmbed.Error(
                    "Not connected",
                    "This server has not been connected with Pulse yet.\nUse `/pulse setup`."
                )
                .Build(),
                ephemeral: false
            );
            return;
        }

        await FollowupAsync(
            embed: PulseEmbed.Success(
                "Server connected",
                $"**Company:** {status.CompanyName}\n" +
                $"**Since:** {status.ConnectedAt:dd-MM-yyyy HH:mm}"
            )
            .Build(),
            ephemeral: false
        );
    }
}