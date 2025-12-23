using Pulse.API.Domain.Logging;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Application.Logging;

public class PlatformPerformanceLogger : IPlatformPerformanceLogger
{
    private readonly PulseDbContext _db;
    public PlatformPerformanceLogger(PulseDbContext db) => _db = db;

    public async Task LogAsync(PlatformPerformanceLog log, CancellationToken cancellationToken = default)
    {
        _db.PlatformPerformanceLogs.Add(log);
        await _db.SaveChangesAsync(cancellationToken);
    }
}