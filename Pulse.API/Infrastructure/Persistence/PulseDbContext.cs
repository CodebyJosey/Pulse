using Microsoft.EntityFrameworkCore;
using Pulse.API.Domain.Companies;
using Pulse.API.Domain.Events;
using Pulse.API.Domain.Identities;
using Pulse.API.Infrastructure.Logging;

namespace Pulse.API.Infrastructure.Persistence;

/// <summary>
/// Entity Framework database context for the Pulse API.
/// This context represents the primary continuous layer and is
/// the single source of truth for all Pulse' data.
/// </summary>
public class PulseDbContext : DbContext
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Identity> Identities => Set<Identity>();
    public DbSet<MinecraftAccount> MinecraftAccounts => Set<MinecraftAccount>();
    public DbSet<DiscordAccount> DiscordAccounts => Set<DiscordAccount>();
    public DbSet<PulseEvent> Events => Set<PulseEvent>();
    public DbSet<AccessLog> AccessLogs => Set<AccessLog>();

    /// <summary>
    /// Initializes a new instance of <see cref="PulseDbContext"/>.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public PulseDbContext(DbContextOptions<PulseDbContext> options) : base(options)
    { }

    /// <summary>
    /// Configures entity mapping and database conventions.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PulseDbContext).Assembly
        );

        modelBuilder.AddLogging();
    }
}