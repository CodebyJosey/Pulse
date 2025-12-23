using Pulse.API.Domain.Logging;

namespace Pulse.API.Application.Logging;

public interface ICompanyLogService
{
    Task LogAsync(CompanyLog log, CancellationToken cancellationToken = default);
}