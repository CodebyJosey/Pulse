using Microsoft.EntityFrameworkCore;
using Pulse.API.Common.Errors;
using Pulse.API.Domain.Identities;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Domain.Events;

/// <summary>
/// Handles creation and persistence of Pulse events.
/// </summary>
public class EventService
{
    private readonly PulseDbContext _db;

    public EventService(PulseDbContext db)
    {
        _db = db;
    }

    public async Task<PulseEvent> CreateAsync(
        Guid companyId,
        Guid? identityId,
        EventPlatform platform,
        PulseEventType type,
        DateTimeOffset occurredAt,
        string payload)
    {
        bool companyExists = await _db.Companies.AnyAsync(company => company.Id == companyId);

        if (!companyExists)
        {
            throw new NotFoundException("Company does not exist.");
        }

        Identity? identity = null;

        if (identityId.HasValue)
        {
            identity = await _db.Identities
                .FirstOrDefaultAsync(identity =>
                    identity.Id == identityId &&
                    identity.CompanyId == companyId);

            if (identity == null)
            {
                throw new NotFoundException("Identity not found in company.");
            }
        }

        PulseEvent pulseEvent = new PulseEvent
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            IdentityId = identity!.Id,
            Type = type,
            OccurredAt = occurredAt,
            Payload = payload
        };

        _db.Events.Add(pulseEvent);
        await _db.SaveChangesAsync();

        return pulseEvent;
    }
}