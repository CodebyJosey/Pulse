using Pulse.API.Domain.Users;

namespace Pulse.API.Domain.Companies;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Guid OwnerUserId { get; set; }
    public User OwnerUser { get; set; } = null!;
}
