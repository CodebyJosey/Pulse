using Microsoft.EntityFrameworkCore;
using Pulse.API.Infrastructure.Persistence;
using Pulse.API.Domain.Identities;

namespace Pulse.API.Services;

public class IdentityLookupService
{
    private readonly PulseDbContext _db;

    public IdentityLookupService(PulseDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Finds an identity by Minecraft username.
    /// </summary>
    public async Task<Identity?> FindByMinecraftUsernameAsync(string username)
    {
        return await _db.Identities
            .Include(identity => identity.MinecraftAccount)
            .Include(identity => identity.DiscordAccount)
            .Where(identity => identity.MinecraftAccount != null)
            .FirstOrDefaultAsync(identity =>
                identity.MinecraftAccount!.Username.ToLower() == username.ToLower());
    }

    public async Task<Identity?> FindByDiscordIdAsync(ulong discordId)
    {
        return await _db.Identities
            .Include(x => x.MinecraftAccount)
            .Include(x => x.DiscordAccount)
            .FirstOrDefaultAsync(x =>
                x.DiscordAccount != null &&
                x.DiscordAccount.DiscordId == discordId);
    }
}