using Pulse.API.Domain.Logging;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Application.Logging;

public sealed class CompanyLogService : ICompanyLogService
{
    private readonly PulseDbContext _db;
    public CompanyLogService(PulseDbContext db) => _db = db;

    public async Task LogAsync(CompanyLog log, CancellationToken ct = default)
    {
        _db.CompanyLogs.Add(log);
        await _db.SaveChangesAsync(ct);
    }
}
