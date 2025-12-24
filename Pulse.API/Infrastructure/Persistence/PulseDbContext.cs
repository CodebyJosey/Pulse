using Microsoft.EntityFrameworkCore;
using Pulse.API.Domain.Bots;
using Pulse.API.Domain.Companies;
using Pulse.API.Domain.Guilds;
using Pulse.API.Domain.Logging;
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
    public DbSet<PlatformPerformanceLog> PlatformPerformanceLogs => Set<PlatformPerformanceLog>();
    public DbSet<PlatformAuditLog> PlatformAuditLogs => Set<PlatformAuditLog>();
    public DbSet<CompanyLog> CompanyLogs => Set<CompanyLog>();
    public DbSet<GuildLoggingSettings> GuildLoggingSettings => Set<GuildLoggingSettings>();


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

        modelBuilder.Entity<PlatformPerformanceLog>()
    .HasIndex(x => x.Timestamp);

        modelBuilder.Entity<PlatformAuditLog>()
            .HasIndex(x => x.Timestamp);

        modelBuilder.Entity<CompanyLog>()
            .HasIndex(x => new { x.CompanyId, x.Timestamp });

        modelBuilder.Entity<CompanyLog>()
            .HasIndex(x => new { x.GuildId, x.Timestamp });

        base.OnModelCreating(modelBuilder);
    }
}
