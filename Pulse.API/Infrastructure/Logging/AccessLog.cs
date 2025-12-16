namespace Pulse.API.Infrastructure.Logging;

/// <summary>
/// Represents a single HTTP access log entry.
/// Used for auditing and performance analysis.
/// </summary>
public class AccessLog
{
    public Guid Id { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public string Method { get; set; } = null!;
    public string Path { get; set; } = null!;

    public int StatusCode { get; set; }

    public string? IpAddress { get; set; }

    /// <summary>
    /// Company resolved from authentication (if any).
    /// </summary>
    public Guid? CompanyId { get; set; }

    public long DurationMs { get; set; }
}
