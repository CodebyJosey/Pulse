using Microsoft.EntityFrameworkCore;
using Pulse.API.Domain.Bots;
using Pulse.API.Domain.Companies;
using Pulse.API.Domain.Guilds;
using Pulse.API.Domain.Modules;
using Pulse.API.Domain.Users;

namespace Pulse.API.Infrastructure.Persistence;

public class PulseDbContext : DbContext
{
    public PulseDbContext(DbContextOptions<PulseDbContext> options) : base(options) { }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<BotAgent> BotAgents => Set<BotAgent>();
    public DbSet<GuildConnection> GuildConnections => Set<GuildConnection>();
    public DbSet<ModuleDefinition> Modules => Set<ModuleDefinition>();
    public DbSet<GuildModuleState> GuildModules => Set<GuildModuleState>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>()
            .HasOne(c => c.OwnerUser)
            .WithMany()
            .HasForeignKey(c => c.OwnerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // -----------------------
        // MODULES
        // -----------------------

        modelBuilder.Entity<ModuleDefinition>()
            .HasIndex(m => m.Key)
            .IsUnique();

        modelBuilder.Entity<GuildModuleState>()
            .HasIndex(gm => new { gm.GuildId, gm.ModuleId })
            .IsUnique();

        modelBuilder.Entity<GuildModuleState>()
            .HasOne(gm => gm.Module)
            .WithMany()
            .HasForeignKey(gm => gm.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
