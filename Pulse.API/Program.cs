using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pulse.API.Infrastructure.Persistence;
using Pulse.API.Security;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

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

// Controllers
builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<PulseDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PulseDatabase")
    );
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

// Swagger (JWT support)
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
});

// =======================
// BUILD
// =======================
WebApplication? app = builder.Build();

// =======================
// DATABASE SEED (modules e.d.)
// =======================
using (IServiceScope? scope = app.Services.CreateScope())
{
    PulseDbContext? db = scope.ServiceProvider.GetRequiredService<PulseDbContext>();
    db.Database.Migrate();
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
app.UseAuthorization();

app.MapControllers();

app.Run();
