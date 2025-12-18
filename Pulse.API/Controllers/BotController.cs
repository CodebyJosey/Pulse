using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pulse.API.Domain.Bots;
using Pulse.API.Domain.Guilds;
using Pulse.API.Infrastructure.Auth;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Controllers;

[ApiController]
[Route("api/bot")]
public class BotController : ControllerBase
{
    private readonly PulseDbContext _db;
    public BotController(PulseDbContext db) => _db = db;

    [HttpPost("claim")]
    public async Task<IActionResult> Claim(
        [FromHeader(Name = "X-BOT-KEY")] string apiKey,
        [FromBody] string guildId)
    {
        string hash = BotKeyHasher.Hash(apiKey);

        BotAgent? bot = await _db.BotAgents.FirstOrDefaultAsync(b => b.ApiKeyHash == hash && b.Active);
        if (bot is null) return Unauthorized();

        bool exists = await _db.GuildConnections
            .AnyAsync(g => g.GuildId == guildId);

        if (exists) return Conflict("Guild already claimed");

        _db.GuildConnections.Add(new GuildConnection
        {
            Id = Guid.NewGuid(),
            CompanyId = bot.CompanyId,
            GuildId = guildId
        });

        await _db.SaveChangesAsync();
        return Ok(new { status = "claimed" });
    }
}
