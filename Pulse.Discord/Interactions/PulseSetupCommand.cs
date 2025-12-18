using Discord.Interactions;
using Pulse.Discord.Client;

namespace Pulse.Discord.Interactions;

public class PulseSetupCommand : InteractionModuleBase<SocketInteractionContext>
{
    private readonly PulseApiClient _api;
    public PulseSetupCommand(PulseApiClient api) => _api = api;

    [SlashCommand("pulse-setup", "Koppel deze server aan Pulse")]
    public async Task Setup(string apiKey)
    {
        await _api.ClaimAsync(apiKey, Context.Guild.Id.ToString());
        await RespondAsync("✅ Server gekoppeld aan Pulse", ephemeral: true);
    }
}
