using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pulse.API.Contracts.Modules;
using Pulse.API.Domain.Modules;
using Pulse.API.Infrastructure.Auth;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Controllers;

[ApiController]
[Route("api/guilds/{guildId}/modules")]
[BotAuth(requireGuildOwnership: false)]
public class ModulesController : ControllerBase
{
    private readonly PulseDbContext _db;
    public ModulesController(PulseDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<GuildModuleDto>>> GetModules(string guildId)
    {
        // 1) alle module defs
        List<ModuleDefinition> defs = await _db.Modules
            .AsNoTracking()
            .OrderBy(m => m.Key)
            .ToListAsync();

        // 2) alle states voor deze guild
        Dictionary<Guid, bool> states = await _db.GuildModules
            .AsNoTracking()
            .Where(gm => gm.GuildId == guildId)
            .ToDictionaryAsync(gm => gm.ModuleId, gm => gm.Enabled);

        // 3) map
        List<GuildModuleDto> result = defs
            .Select(m => new GuildModuleDto(
                m.Key,
                m.Name,
                m.Description,
                states.TryGetValue(m.Id, out bool enabled) && enabled
            ))
            .ToList();

        return Ok(result);
    }

    [HttpPost("{moduleKey}/enable")]
    public async Task<IActionResult> EnableModule(string guildId, string moduleKey)
    {
        await SetModuleState(guildId, moduleKey, true);
        return NoContent();
    }

    [HttpPost("{moduleKey}/disable")]
    public async Task<IActionResult> DisableModule(string guildId, string moduleKey)
    {
        await SetModuleState(guildId, moduleKey, false);
        return NoContent();
    }

    private async Task SetModuleState(string guildId, string moduleKey, bool enabled)
    {
        ModuleDefinition module = await _db.Modules
            .FirstOrDefaultAsync(m => m.Key == moduleKey)
            ?? throw new InvalidOperationException("Module does not exist.");

        GuildModuleState? state = await _db.GuildModules
            .FirstOrDefaultAsync(gm => gm.GuildId == guildId && gm.ModuleId == module.Id);

        if (state is null)
        {
            state = new GuildModuleState
            {
                Id = Guid.NewGuid(),
                GuildId = guildId,
                ModuleId = module.Id,
                Enabled = enabled
            };

            _db.GuildModules.Add(state);
        }
        else
        {
            state.Enabled = enabled;
        }

        await _db.SaveChangesAsync();
    }
}
