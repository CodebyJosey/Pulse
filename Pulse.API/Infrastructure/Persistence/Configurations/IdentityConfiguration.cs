using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pulse.API.Domain.Identities;

namespace Pulse.API.Infrastructure.Persistence.Configurations;

public class IdentityConfiguration : IEntityTypeConfiguration<Identity>
{
    public void Configure(EntityTypeBuilder<Identity> entity)
    {
        entity.HasKey(x => x.Id);

        entity.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId);

        entity.HasOne(x => x.MinecraftAccount)
            .WithOne(x => x.Identity)
            .HasForeignKey<MinecraftAccount>(x => x.IdentityId);

        entity.HasOne(x => x.DiscordAccount)
            .WithOne(x => x.Identity)
            .HasForeignKey<DiscordAccount>(x => x.IdentityId);
    }
}
