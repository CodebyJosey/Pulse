using System.Security.Cryptography;
using System.Text;

namespace Pulse.API.Security;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        using SHA256 sha = SHA256.Create();
        byte[]? bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public static bool Verify(string password, string hash)
    {
        return Hash(password) == hash;
    }
}