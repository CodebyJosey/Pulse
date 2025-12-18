using System.Security.Cryptography;
using System.Text;

namespace Pulse.API.Infrastructure.Auth;

public static class BotKeyHasher
{
    public static string Hash(string key)
    {
        using SHA256? sha = SHA256.Create();
        return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(key)));
    }
}
