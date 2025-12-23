using Pulse.API.Domain.Logging;

namespace Pulse.API.Application.Logging;

public interface IPlatformPerformanceLogger
{
    Task LogAsync(PlatformPerformanceLog log, CancellationToken cancellationToken = default);
}