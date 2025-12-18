using Microsoft.EntityFrameworkCore;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Builders;

/// <summary>
/// Registers core Pulse API services.
/// </summary>
public sealed class PulseApiBuilder
{
    private readonly WebApplicationBuilder _builder;

    private PulseApiBuilder(WebApplicationBuilder builder)
    {
        _builder = builder;
    }

    public static PulseApiBuilder Create(WebApplicationBuilder builder)
        => new(builder);

    /// <summary>
    /// Registers PostgreSQL + EF Core.
    /// </summary>
    public PulseApiBuilder AddDatabase()
    {
        string? connectionString =
            _builder.Configuration.GetConnectionString("PulseDatabase");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                "Connection string 'PulseDatabase' is not configured.");

        _builder.Services.AddDbContext<PulseDbContext>(options =>
            options.UseNpgsql(connectionString));

        return this;
    }

    /// <summary>
    /// Registers Swagger.
    /// </summary>
    public PulseApiBuilder AddSwagger()
    {
        _builder.Services.AddEndpointsApiExplorer();
        _builder.Services.AddSwaggerGen();
        return this;
    }
}
