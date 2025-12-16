namespace Pulse.API.Domain.Identities;

/// <summary>
/// Represents a linked Minecraft account.
/// </summary>
public class MinecraftAccount
{
    /// <summary>
    /// Minecraft UUID.
    /// </summary>
    public Guid Uuid { get; set; }

    /// <summary>
    /// Last known Minecraft username.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Identity this Minecraft account belongs to.
    /// </summary>
    public Guid IdentityId { get; set; }

    public Identity Identity { get; set; } = null!;
}
