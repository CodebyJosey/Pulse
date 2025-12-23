using Pulse.API.Domain.Logging;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Application.Logging;

public sealed class PlatformAuditLogger : IPlatformAuditLogger
{
    private readonly PulseDbContext _db;
    public PlatformAuditLogger(PulseDbContext db) => _db = db;

    public async Task LogAsync(PlatformAuditLog log, CancellationToken ct = default)
    {
        _db.PlatformAuditLogs.Add(log);
        await _db.SaveChangesAsync(ct);
    }
}
