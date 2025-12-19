using Discord;

namespace Pulse.Discord.UI.Embeds;

public static class PulseEmbed
{
    private static readonly Color PulseColor = new(88, 101, 242);

    public static EmbedBuilder Base(string title)
    {
        return new EmbedBuilder()
            .WithTitle(title)
            .WithColor(PulseColor)
            .WithFooter("Pulse • Management Platform")
            .WithCurrentTimestamp();
    }

    public static EmbedBuilder Success(string title, string description)
    {
        return Base(title)
            .WithDescription(description)
            .WithColor(Color.Green);
    }

    public static EmbedBuilder Error(string title, string description)
    {
        return Base(title)
            .WithDescription(description)
            .WithColor(Color.Red);
    }

    public static EmbedBuilder Info(string title, string description)
    {
        return Base(title)
            .WithDescription(description);
    }
}