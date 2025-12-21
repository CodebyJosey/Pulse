using Microsoft.EntityFrameworkCore;
using Pulse.API.Domain.Modules;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Infrastructure.Seeding;

public static class ModuleSeeder
{
    public static async Task SeedAsync(PulseDbContext db)
    {
        bool any = await db.Modules.AsNoTracking().AnyAsync();
        if (any) return;

        db.Modules.AddRange(new[]
        {
            new ModuleDefinition
            {
                Id = Guid.NewGuid(),
                Key = "core",
                Name = "Core",
                Description = "Basis functionaliteit van de bot."
            },
            new ModuleDefinition
            {
                Id = Guid.NewGuid(),
                Key = "logging",
                Name = "Logging",
                Description = "Logging & audit events."
            },
            new ModuleDefinition
            {
                Id = Guid.NewGuid(),
                Key = "moderation",
                Name = "Moderation",
                Description = "Moderatie commands zoals kick/ban/timeouts."
            },
            new ModuleDefinition
            {
                Id = Guid.NewGuid(),
                Key = "fun",
                Name = "Fun",
                Description = "Fun commands zoals ping, memes, etc."
            }
        });

        await db.SaveChangesAsync();
    }
}
