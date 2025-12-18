using Microsoft.EntityFrameworkCore;
using Pulse.API.Domain.Bots;
using Pulse.API.Domain.Companies;
using Pulse.API.Domain.Guilds;

namespace Pulse.API.Infrastructure.Persistence;

public class PulseDbContext : DbContext
{
    public PulseDbContext(DbContextOptions<PulseDbContext> options) : base(options) { }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<BotAgent> BotAgents => Set<BotAgent>();
    public DbSet<GuildConnection> GuildConnections => Set<GuildConnection>();
}
