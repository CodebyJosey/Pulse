using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pulse.API.Domain.Identities;

namespace Pulse.API.Infrastructure.Persistence.Configurations;

public class MinecraftAccountConfiguration : IEntityTypeConfiguration<MinecraftAccount>
{
    public void Configure(EntityTypeBuilder<MinecraftAccount> entity)
    {
        entity.HasKey(x => x.Uuid);

        entity.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(32);
    }
}
