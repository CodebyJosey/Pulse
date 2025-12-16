using Pulse.API.Builders;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = PulseApiBuilder
    .Create(builder)
    .AddDatabase()
    .AddIdentityServices()
    .AddEventServices()
    .AddEventQueryServices()
    .UseRequestLogging()
    .UseExceptionHandling()
    .AddSwagger()
    .Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();