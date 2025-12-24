using Discord;
using Discord.Rest;
using Discord.WebSocket;

namespace Pulse.Discord.Services;

public sealed class DiscordLogChannelService
{
    private readonly DiscordSocketRestClient _rest;
    private readonly GuildLoggingSettingsService _settings;

    public DiscordLogChannelService(
        DiscordSocketRestClient rest,
        GuildLoggingSettingsService settings)
    {
        _rest = rest;
        _settings = settings;
    }

    public async Task TryLogAsync(ulong guildId, Embed embed)
    {
        ulong? channelId = await _settings.GetLogChannelAsync(guildId);

        if (channelId is null)
        {
            return;
        }

        try
        {
            RestChannel? channel = await _rest.GetChannelAsync(channelId.Value);

            if (channel is not IMessageChannel messageChannel)
            {
                Console.WriteLine("[DiscordLog] NOT IMessageChannel");
                return;
            }

            await messageChannel.SendMessageAsync(embed: embed);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DiscordLog] FAILED: {ex}");
        }
    }
}
