using Microsoft.EntityFrameworkCore;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Domain.Events;

/// <summary>
/// Provides read-only access to Pulse events.
/// </summary>
public class EventQueryService
{
    private readonly PulseDbContext _db;

    public EventQueryService(PulseDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<PulseEvent>> GetAllAsync(
        int limit = 100
    )
    {
        return await _db.Events
            .OrderByDescending(pulseEvent => pulseEvent.OccurredAt)
            .Take(limit)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<PulseEvent>> GetByCompanyAsync(
        Guid companyId,
        EventPlatform? platform = null,
        PulseEventType? type = null,
        int limit = 100
    )
    {
        IQueryable<PulseEvent> query = _db.Events
            .Where(pulseEvent => pulseEvent.CompanyId == companyId);

        if (platform.HasValue)
        {
            query = query.Where(pulseEvent => pulseEvent!.Platform == platform);
        }

        if (type.HasValue)
        {
            query = query.Where(pulseEvent => pulseEvent!.Type == type);
        }

        return await query
            .OrderByDescending(pulseEvent => pulseEvent.OccurredAt)
            .Take(limit)
            .AsNoTracking()
            .ToListAsync();
    }
}