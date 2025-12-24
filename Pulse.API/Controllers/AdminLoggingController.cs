using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pulse.API.Contracts.Logging;
using Pulse.API.Domain.Logging;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Controllers;

[ApiController]
[Route("api/admin/guilds/{guildId}/logging")]
[Authorize]
public class AdminLoggingController : ControllerBase
{
    private readonly PulseDbContext _db;

    public AdminLoggingController(PulseDbContext db)
    {
        _db = db;
    }

    [HttpPut("channel")]
    public async Task<IActionResult> SetChannel(
        string guildId,
        [FromBody] SetLogChannelRequest req)
    {
        GuildLoggingSettings? settings = await _db.GuildLoggingSettings
            .FirstOrDefaultAsync(x => x.GuildId == guildId);

        if (settings is null)
        {
            settings = new GuildLoggingSettings
            {
                Id = Guid.NewGuid(),
                GuildId = guildId,
                LogChannelId = req.ChannelId,
                UpdatedAt = DateTimeOffset.UtcNow
            };
            _db.GuildLoggingSettings.Add(settings);
        }
        else
        {
            settings.LogChannelId = req.ChannelId;
            settings.UpdatedAt = DateTimeOffset.UtcNow;
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }
}
