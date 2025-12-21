using System.Text.Json;

namespace Pulse.Discord.Services;

public sealed class BotKeyStore
{
    private readonly string _path = Path.Combine("data", "botkeys.json");
    private readonly Dictionary<ulong, string> _keys = new Dictionary<ulong, string>();

    public BotKeyStore()
    {
        Directory.CreateDirectory("data");

        if (File.Exists(_path))
        {
            string json = File.ReadAllText(_path);
            Dictionary<ulong, string>? loaded = JsonSerializer.Deserialize<Dictionary<ulong, string>>(json);
            if (loaded is not null)
            {
                foreach (KeyValuePair<ulong, string> keyValuePair in loaded)
                {
                    _keys[keyValuePair.Key] = keyValuePair.Value;
                }
            }
        }
    }

    public string Get(ulong guildId)
        => _keys.TryGetValue(guildId, out string? key) ? key : null!;

    public void Set(ulong guildId, string apiKey)
    {
        _keys[guildId] = apiKey;
        Save();
    }
    
    private void Save()
    {
        string json = JsonSerializer.Serialize(_keys, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(_path, json);
    }
}