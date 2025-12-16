using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pulse.API.Domain.Identities;

namespace Pulse.API.Infrastructure.Persistence.Configurations;

public class DiscordAccountConfiguration : IEntityTypeConfiguration<DiscordAccount>
{
    public void Configure(EntityTypeBuilder<DiscordAccount> entity)
    {
        entity.HasKey(x => x.DiscordId);

        entity.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(100);
    }
}
