using Pulse.API.Domain.Logging;

namespace Pulse.API.Application.Logging;

public interface IPlatformAuditLogger
{
    Task LogAsync(PlatformAuditLog log, CancellationToken cancellationToken = default);
}