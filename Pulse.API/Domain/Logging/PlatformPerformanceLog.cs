namespace Pulse.API.Domain.Logging;

public class PlatformPerformanceLog
{
    public Guid Id { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string Source { get; set; } = "API"; // API / BOT / WORKER

    public string Method { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public int? StatusCode { get; set; }

    public long DurationMs { get; set; }
    public bool Success { get; set; }

    public Guid? CompanyId { get; set; }
    public Guid? UserId { get; set; }
    public string? GuildId { get; set; }

    public string? MetadataJson { get; set; }
}