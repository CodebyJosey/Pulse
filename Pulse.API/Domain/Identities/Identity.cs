using Pulse.API.Domain.Companies;

namespace Pulse.API.Domain.Identities;

public class Identity
{
    /// <summary>
    /// Unique identifier of the identity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Company this identity belongs to.
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// Optional display name for internal use.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// UTC timestamp when the identity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Company Company { get; set; } = null!;

    public MinecraftAccount? MinecraftAccount { get; set; }
    public DiscordAccount? DiscordAccount { get; set; }
}