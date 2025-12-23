using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pulse.API.Contracts.Modules;
using Pulse.API.Domain.Modules;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Controllers;

[ApiController]
[Route("api/admin/guilds/{guildId}/modules")]
[Authorize] // 🔐 JWT REQUIRED
public class AdminModulesController : ControllerBase
{
    private readonly PulseDbContext _db;

    public AdminModulesController(PulseDbContext db)
    {
        _db = db;
    }

    // ============================
    // LIST MODULES (ADMIN)
    // ============================
    [HttpGet]
    public async Task<ActionResult> GetModules(string guildId)
    {
        List<ModuleDefinition>? defs = await _db.Modules
            .AsNoTracking()
            .OrderBy(m => m.Key)
            .ToListAsync();

        Dictionary<Guid, GuildModuleState>? states = await _db.GuildModules
            .AsNoTracking()
            .Where(gm => gm.GuildId == guildId)
            .ToDictionaryAsync(gm => gm.ModuleId, gm => gm);

        IEnumerable<GuildModuleDto>? result = defs.Select(m =>
        {
            states.TryGetValue(m.Id, out var state);

            return new GuildModuleDto
            (
                m.Key,
                m.Name,
                m.Description,
                state?.Enabled ?? false,
                state?.UpdatedAt ?? DateTimeOffset.MinValue
            );
        });

        return Ok(result);
    }

    // ============================
    // SET MODULE STATE (ADMIN)
    // ============================
    [HttpPut("{moduleKey}")]
    public async Task<IActionResult> SetModule(
        string guildId,
        string moduleKey,
        [FromBody] SetModuleRequest request)
    {
        ModuleDefinition? module = await _db.Modules
            .FirstOrDefaultAsync(m => m.Key == moduleKey);

        if (module is null)
            return NotFound("Module does not exist.");

        GuildModuleState? state = await _db.GuildModules
            .FirstOrDefaultAsync(gm =>
                gm.GuildId == guildId &&
                gm.ModuleId == module.Id);

        if (state is null)
        {
            state = new GuildModuleState
            {
                Id = Guid.NewGuid(),
                GuildId = guildId,
                ModuleId = module.Id,
                Enabled = request.Enabled,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            _db.GuildModules.Add(state);
        }
        else
        {
            state.Enabled = request.Enabled;
            state.UpdatedAt = DateTimeOffset.UtcNow;
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }
}
