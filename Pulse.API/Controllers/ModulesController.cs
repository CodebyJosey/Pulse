using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pulse.API.Contracts.Modules;
using Pulse.API.Domain.Modules;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Controllers;

[ApiController]
[Route("api/guilds/{guildId}/modules")]
public class ModulesController : ControllerBase
{
    private readonly PulseDbContext _db;

    public ModulesController(PulseDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<GuildModuleDto>>> GetModules(string guildId)
    {
        List<GuildModuleDto>? modules = await _db.Modules
            .Select(module => new GuildModuleDto(
                module.Key, module.Name, module.Description,
                _db.GuildModules.Any(guildModule =>
                    guildModule.GuildId == guildId &&
                    guildModule.ModuleId == module.Id &&
                    guildModule.Enabled)
            ))
            .ToListAsync();

        return Ok(modules);
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
        ModuleDefinition? module = await _db.Modules
            .FirstOrDefaultAsync(module => module.Key == moduleKey)
            ?? throw new InvalidOperationException("Module does not exist.");

        GuildModuleState? state = await _db.GuildModules
            .FirstOrDefaultAsync(guildModule =>
                guildModule.GuildId == guildId &&
                guildModule.ModuleId == module.Id);

        if (state is null)
        {
            state = new GuildModuleState
            {
                GuildId = guildId,
                ModuleId = module.Id,
                Enabled = enabled
            };
        }
        else
        {
            state.Enabled = enabled;
        }

        await _db.SaveChangesAsync();
    }
}