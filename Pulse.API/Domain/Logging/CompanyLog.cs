namespace Pulse.API.Domain.Logging;

public class CompanyLog
{
    public Guid Id { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    public Guid CompanyId { get; set; }
    public string GuildId { get; set; } = string.Empty;

    public string ModuleKey { get; set; } = string.Empty; // fun, moderation, logging
    public string EventType { get; set; } = string.Empty; // CommandExecuted, UserBanned, TicketCreated

    public string Message { get; set; } = string.Empty;
    public string? MetadataJson { get; set; }
}