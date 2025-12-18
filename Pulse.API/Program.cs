using Pulse.API.Builders;

var builder = WebApplication.CreateBuilder(args);

// CONFIG
builder.Configuration
    .AddJsonFile("Properties/appsettings.json", optional: false)
    .AddJsonFile("Properties/appsettings.Development.json", optional: true);

// SERVICES
builder.Services.AddControllers();

PulseApiBuilder
    .Create(builder)
    .AddDatabase()
    .AddSwagger();

// BUILD
WebApplication app = builder.Build();

// PIPELINE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
