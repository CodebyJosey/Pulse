using Microsoft.EntityFrameworkCore;
using Pulse.API.Common.Errors;
using Pulse.API.Domain.Events;
using Pulse.API.Infrastructure.Logging;
using Pulse.API.Infrastructure.Persistence;
using Pulse.API.Services;

namespace Pulse.API.Builders;

/// <summary>
/// Builder responsible for configuring and bootstrapping
/// the Pulse API Infrastructure.
/// </summary>
public sealed class PulseApiBuilder
{
    private readonly WebApplicationBuilder _builder;
    private WebApplication _app;

    public IServiceCollection Services => _builder.Services;

    /// <summary>
    /// Initializes a new instance of the <see cref="PulseApiBuilder"/>.
    /// </summary>
    /// <param name="builder">The underlying ASP.NET application builder.</param>
    private PulseApiBuilder(WebApplicationBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>
    /// Creates a new <see cref="PulseApiBuilder"/> instance.
    /// </summary>
    /// <param name="builder">The ASP.NET application builder.</param>
    public static PulseApiBuilder Create(WebApplicationBuilder builder)
    {
        return new PulseApiBuilder(builder);
    }

    /// <summary>
    /// Creates a new <see cref="PulseApiBuilder"/> instance.
    /// </summary>
    public PulseApiBuilder AddDatabase()
    {
        _builder.Configuration
            .AddJsonFile("Properties/appsettings.json", optional: false)
            .AddJsonFile("Properties/appsettings.Development.json", optional: true);

        string connectionString = _builder.Configuration.GetConnectionString("PulseDatabase")!;

        if(string.IsNullOrWhiteSpace(connectionString)) 
        {
            throw new InvalidOperationException("Connection string 'PulseDatabase' is not configured.");
        }

        _builder.Services.AddDbContext<PulseDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return this;
    }

    /// <summary>
    /// Registers Swagger and XML documentation support.
    /// </summary>
    public PulseApiBuilder AddSwagger()
    {
        _builder.Services.AddEndpointsApiExplorer();
        _builder.Services.AddSwaggerGen(options =>
        {
            string xmlFile = $"{typeof(PulseApiBuilder).Assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
        });

        return this;
    }

    /// <summary>
    /// Registers Identity Services.
    /// </summary>
    public PulseApiBuilder AddIdentityServices()
    {
        _builder.Services.AddScoped<IdentityLookupService>();
        return this;
    }

    /// <summary>
    /// Registers Event Services.
    /// </summary>
    public PulseApiBuilder AddEventServices()
    {
        _builder.Services.AddScoped<EventService>();
        return this;
    }

    public PulseApiBuilder AddEventQueryServices()
    {
        _builder.Services.AddScoped<EventQueryService>();
        return this;
    }

    /// <summary>
    /// Registers exception handling.
    /// </summary>
    public PulseApiBuilder UseExceptionHandling()
    {
        Build().UseMiddleware<ExceptionMiddleware>();
        return this;
    }

    /// <summary>
    /// Registers request logging as a middleware.
    /// </summary>
    public PulseApiBuilder UseRequestLogging()
    {
        Build().UseMiddleware<RequestLoggingMiddleware>();
        return this;
    }

    /// <summary>
    /// Builds the configured <see cref="WebApplication"/>.
    /// </summary>
    public WebApplication Build()
    {
        _app ??= _builder.Build();
        return _app;
    }
}