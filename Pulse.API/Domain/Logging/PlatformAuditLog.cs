namespace Pulse.API.Domain.Logging;

public class PlatformAuditLog
{
    public Guid Id { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    public string Category { get; set; } = string.Empty; // Admin, Security, System
    public string Action { get; set; } = string.Empty; // ModuleToggled, GuildLinked, TokenCreated

    public string Level { get; set; } = "Information"; // Information, Error, Warning, ...
    public string Message { get; set; } = string.Empty;

    public Guid? CompanyId { get; set; }
    public Guid? UserId { get; set; }
    public string? GuildId { get; set; }

    public string? MetadataJson { get; set; }
}