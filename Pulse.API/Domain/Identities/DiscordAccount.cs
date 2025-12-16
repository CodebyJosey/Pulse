namespace Pulse.API.Domain.Identities;

/// <summary>
/// Represents a linked Discord account.
/// </summary>
public class DiscordAccount
{
    /// <summary>
    /// Discord user ID.
    /// </summary>
    public ulong DiscordId { get; set; }

    /// <summary>
    /// Last known Discord username.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Identity this Discord account belongs to.
    /// </summary>
    public Guid IdentityId { get; set; }

    public Identity Identity { get; set; } = null!;
}
