using System.Net.Http.Json;

namespace Pulse.Discord.Client;

public class PulseApiClient
{
    private readonly HttpClient _http;
    public PulseApiClient(HttpClient http) => _http = http;

    public async Task PostAsync<T>(string url, T body, string? apiKey = null)
    {
        _http.DefaultRequestHeaders.Remove("X-BOT-KEY");
        _http.DefaultRequestHeaders.Add("X-BOT-KEY", apiKey);

        HttpResponseMessage res = await _http.PostAsJsonAsync(url, body);
        res.EnsureSuccessStatusCode();
    }

    public async Task<T> GetAsync<T>(string url)
    {
        T? result = await _http.GetFromJsonAsync<T>(url);

        if (result is null)
        {
            throw new InvalidOperationException("Empty API response!");
        }

        return result;
    }
}
