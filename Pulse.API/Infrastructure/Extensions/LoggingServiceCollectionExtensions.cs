using Microsoft.Extensions.DependencyInjection;
using Pulse.API.Application.Logging;

namespace Pulse.API.Infrastructure.Extensions;

public static class LoggingServiceCollectionExtensions
{
    public static IServiceCollection AddPulseLogging(this IServiceCollection services)
    {
        services.AddScoped<IPlatformPerformanceLogger, PlatformPerformanceLogger>();
        services.AddScoped<IPlatformAuditLogger, PlatformAuditLogger>();
        services.AddScoped<ICompanyLogService, CompanyLogService>();
        return services;
    }
}
