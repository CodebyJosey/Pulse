using Discord;
using Discord.Interactions;
using Discord.Rest;
using Pulse.Discord.Client;
using Pulse.Discord.Services;

namespace Pulse.Discord.Interactions.Admin;

[DefaultMemberPermissions(GuildPermission.Administrator)]
public class SetLogChannelCommand : InteractionModuleBase<SocketInteractionContext>
{
    private readonly PulseApiClient _api;
    private readonly BotKeyStore _keys;
    private readonly GuildLoggingSettingsService _settings;

    public SetLogChannelCommand(
        PulseApiClient api,
        BotKeyStore keys,
        GuildLoggingSettingsService settings)
    {
        _api = api;
        _keys = keys;
        _settings = settings;
    }

    [SlashCommand("setlogchannel", "Configure the logging channel for Pulse")]
    public async Task SetLogChannel(
    [Summary("channel", "The channel to send logs to (or null to turn off)")]
    ITextChannel? channel = null)
    {
        string? apiKey = _keys.Get(Context.Guild.Id);
        if (apiKey is null)
        {
            await RespondAsync(
                "❌ This guild is not connected to Pulse yet.",
                ephemeral: true
            );
            return;
        }

        // 🔥 BELANGRIJK: meteen deferen
        await DeferAsync(ephemeral: true);

        // API call (mag nu lang duren)
        await _api.PutAsync(
            $"api/bot/guilds/{Context.Guild.Id}/logging/channel",
            new
            {
                ChannelId = channel?.Id
            },
            apiKey
        );

        // cache resetten
        _settings.Invalidate(Context.Guild.Id);

        // 🔥 Follow-up i.p.v. RespondAsync
        await FollowupAsync(
            channel is null
                ? "🛑 Logging channel is disabled."
                : $"✅ Logging channel is configured at {channel.Mention}.",
            ephemeral: true
        );
    }
}
