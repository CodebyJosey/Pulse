using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pulse.API.Domain.Events;

namespace Pulse.API.Infrastructure.Persistence.Configurations;

public class PulseEventConfiguration : IEntityTypeConfiguration<PulseEvent>
{
    public void Configure(EntityTypeBuilder<PulseEvent> entity)
    {
        entity.ToTable("events");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Type)
            .IsRequired();

        entity.Property(x => x.OccurredAt)
            .IsRequired();

        entity.Property(x => x.Payload)
            .IsRequired()
            .HasColumnType("jsonb");

        // Tenant scoping
        entity.HasIndex(x => x.CompanyId);

        // Time-based queries
        entity.HasIndex(x => x.OccurredAt);

        // Common analytics pattern
        entity.HasIndex(x => new { x.CompanyId, x.Type });

        // Identity is optional, events must survive identity deletion
        entity.HasOne(x => x.Identity)
            .WithMany()
            .HasForeignKey(x => x.IdentityId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
