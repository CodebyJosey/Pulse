using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pulse.API.Contracts.Modules;
using Pulse.API.Domain.Modules;
using Pulse.API.Infrastructure.Auth;
using Pulse.API.Infrastructure.Persistence;

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
            states.TryGetValue(m.Id, out GuildModuleState? state);

            return new GuildModuleDto(
                m.Key,
                m.Name,
                m.Description,
                state?.Enabled ?? false,
                state?.UpdatedAt ?? DateTimeOffset.MinValue
            );
        });

        return Ok(result.ToList());
    }
}
