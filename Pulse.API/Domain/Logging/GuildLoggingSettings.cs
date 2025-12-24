namespace Pulse.API.Domain.Logging;

public class GuildLoggingSettings
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string GuildId { get; set; } = string.Empty;
    public ulong? LogChannelId { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}