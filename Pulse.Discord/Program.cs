using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Pulse.Discord.Client;
using Pulse.Discord.Configuration;
using Pulse.Discord.Extensions;
using Pulse.Discord.Hosting;

EnvironmentLoader.Load();

string apiUrl = "http://localhost:5255/";

IHost? host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(s =>
    {
        s.AddDiscordClient();
        s.AddPulseServices(apiUrl);
        s.AddHostedService<DiscordHostedService>();
    })
    .Build();

await host.RunAsync();
