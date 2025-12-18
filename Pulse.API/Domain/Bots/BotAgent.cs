namespace Pulse.API.Domain.Bots;

public class BotAgent
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string ApiKeyHash { get; set; } = null!;
    public bool Active { get; set; } = true;
}
