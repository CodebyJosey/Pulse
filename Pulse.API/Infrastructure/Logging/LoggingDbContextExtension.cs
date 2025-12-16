using Microsoft.EntityFrameworkCore;

namespace Pulse.API.Infrastructure.Logging;

/// <summary>
/// Extension methods for registering logging entities
/// on the PulseDbContext.
/// </summary>
public static class LoggingDbContextExtension
{
    public static ModelBuilder AddLogging(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessLog>(entity =>
        {
            entity.ToTable("access_logs");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Method)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(x => x.Path)
                .IsRequired()
                .HasMaxLength(512);

            entity.Property(x => x.Timestamp)
                .IsRequired();

            entity.HasIndex(x => x.Timestamp);
            entity.HasIndex(x => x.CompanyId);
        });

        return modelBuilder;
    }
}
