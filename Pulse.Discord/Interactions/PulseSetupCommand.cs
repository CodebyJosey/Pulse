using Discord.Interactions;
using Pulse.Discord.Client;
using Pulse.Discord.Services;
using Pulse.Discord.UI.Embeds;

namespace Pulse.Discord.Interactions;

public class PulseSetupCommand : InteractionModuleBase<SocketInteractionContext>
{
    private readonly PulseApiClient _api;
    private readonly BotKeyStore _keys;
    private readonly ModuleStateService _modules;
    public PulseSetupCommand(PulseApiClient api, BotKeyStore keys, ModuleStateService modules)
    {
        _api = api;
        _keys = keys;
        _modules = modules;
    }

    [SlashCommand("pulse-setup", "Connect this server to Pulse.")]
    public async Task Setup(string apiKey)
    {
        await DeferAsync(ephemeral: false);

        try
        {
            await _api.PostAsync(
                "api/bot/claim",
                Context.Guild.Id.ToString(),
                apiKey
            );

            _keys.Set(Context.Guild.Id, apiKey);
            _modules.Invalidate(Context.Guild.Id);

            await FollowupAsync(
                embed: PulseEmbed.Success(
                    "Setup finished",
                    "This server has been successfully connected to Pulse."
                )
                .Build(),
                ephemeral: false
            );
        }
        catch (HttpRequestException)
        {
            await FollowupAsync(
                embed: PulseEmbed.Error(
                    "Setup failed",
                    "❌ The API key is invalid **or** this server is already connected.\n\n" +
                    "Check the API key in our dashboard."
                ).Build(),
                ephemeral: false
            );
        }
        catch(Exception ex)
        {
            // Onverwachte fout (log je later naar API)
            await FollowupAsync(
                embed: PulseEmbed.Error(
                    "Unexpected error",
                    "Something went wrong while connecting the server to Pulse.\n" +
                    "Please try again later."
                ).Build(),
                ephemeral: false
            );

            Console.WriteLine(ex);
        }
    }
}
