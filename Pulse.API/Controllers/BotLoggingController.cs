using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pulse.API.Contracts.Logging;
using Pulse.API.Domain.Logging;
using Pulse.API.Infrastructure.Auth;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Controllers;

public record GuildLoggingSettingsDto(ulong? LogChannelId, DateTimeOffset UpdatedAt);

[ApiController]
[Route("api/bot/guilds/{guildId}/logging")]
[BotAuth(requireGuildOwnership: true)]
public class BotLoggingController : ControllerBase
{
    private readonly PulseDbContext _db;

    public BotLoggingController(PulseDbContext db)
    {
        _db = db;
    }

    [HttpGet("channel")]
    public async Task<ActionResult<GuildLoggingSettingsDto>> GetChannel(string guildId)
    {
        Guid companyId = (Guid)HttpContext.Items["CompanyId"]!;

        GuildLoggingSettings? settings = await _db.GuildLoggingSettings
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.GuildId == guildId && x.CompanyId == companyId);

        if (settings is null)
        {
            return Ok(new GuildLoggingSettingsDto(null, DateTimeOffset.MinValue));
        }

        return Ok(new GuildLoggingSettingsDto(settings.LogChannelId, settings.UpdatedAt));
    }

    [HttpPut("channel")]
    public async Task<IActionResult> SetLogChannel(
        string guildId,
        [FromBody] SetLogChannelRequest req)
    {
        Guid companyId = (Guid)HttpContext.Items["CompanyId"]!;

        var settings = await _db.GuildLoggingSettings
            .FirstOrDefaultAsync(x =>
                x.GuildId == guildId &&
                x.CompanyId == companyId);

        if (settings is null)
        {
            settings = new GuildLoggingSettings
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId,
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
