using System.Net.Http.Json;

namespace Pulse.Discord.Client;

public class PulseApiClient
{
    private readonly HttpClient _http;
    public PulseApiClient(HttpClient http) => _http = http;

    public async Task ClaimAsync(string apiKey, string guildId)
    {
        _http.DefaultRequestHeaders.Remove("X-BOT-KEY");
        _http.DefaultRequestHeaders.Add("X-BOT-KEY", apiKey);

        HttpResponseMessage res = await _http.PostAsJsonAsync("api/bot/claim", guildId);
        res.EnsureSuccessStatusCode();
    }
}
