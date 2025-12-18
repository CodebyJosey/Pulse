namespace Pulse.API.Domain.Guilds;

public class GuildConnection
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string GuildId { get; set; } = null!;
    public DateTimeOffset ConnectedAt { get; set; } = DateTimeOffset.UtcNow;
}
