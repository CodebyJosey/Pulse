using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pulse.API.Application.Logging;
using Pulse.API.Infrastructure.Logging;
using Pulse.API.Infrastructure.Persistence;
using Pulse.API.Infrastructure.Seeding;
using Pulse.API.Security;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// =======================
// CONFIG
// =======================
builder.Configuration
    .AddJsonFile("Properties/appsettings.json", optional: false)
    .AddJsonFile("Properties/appsettings.Development.json", optional: true)
    .AddEnvironmentVariables();

// =======================
// SERVICES
// =======================
builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<PulseDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PulseDatabase"));
});

// JWT services
builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

// ✅ Logging services (DB)
builder.Services.AddScoped<IPlatformPerformanceLogger, PlatformPerformanceLogger>();
builder.Services.AddScoped<IPlatformAuditLogger, PlatformAuditLogger>();
builder.Services.AddScoped<ICompanyLogService, CompanyLogService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new()
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Voer in: Bearer {token}"
    });

    options.AddSecurityDefinition("BotKey", new()
    {
        Name = "X-BOT-KEY",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Pulse Discord bot API key"
    });

    options.AddSecurityRequirement(new()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    options.AddSecurityRequirement(new()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "BotKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

// =======================
// BUILD
// =======================
WebApplication app = builder.Build();

// =======================
// DATABASE SEED
// =======================
using (IServiceScope scope = app.Services.CreateScope())
{
    PulseDbContext db = scope.ServiceProvider.GetRequiredService<PulseDbContext>();
    db.Database.Migrate();
    await ModuleSeeder.SeedAsync(db);
}

// =======================
// PIPELINE
// =======================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

// ✅ Performance logging (after auth so we can read UserId claims)
app.UseMiddleware<PerformanceLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
