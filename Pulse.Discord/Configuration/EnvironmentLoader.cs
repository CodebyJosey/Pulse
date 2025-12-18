using System.Text;

namespace Pulse.Discord.Configuration;

public static class EnvironmentLoader
{
    public static void Load(string fileName = ".env")
    {
        if (!File.Exists(fileName))
            return;

        foreach (string line in File.ReadAllLines(fileName, Encoding.UTF8))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.TrimStart().StartsWith("#"))
            {
                continue;
            }

            int index = line.IndexOf('=');
            if (index <= 0)
                continue;

            string key = line[..index].Trim();
            string value = line[(index + 1)..].Trim();

            Environment.SetEnvironmentVariable(key, value);
        }
    }
}
