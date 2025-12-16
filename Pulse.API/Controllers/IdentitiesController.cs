using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pulse.API.Infrastructure.Persistence;
using Pulse.API.Domain.Identities;

namespace Pulse.API.Controllers;

/// <summary>
/// Provides access to identities within Pulse.
/// </summary>
[ApiController]
[Route("api/identities")]
public class IdentitiesController : ControllerBase
{
    private readonly PulseDbContext _db;

    public IdentitiesController(PulseDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Retrieves an identity by its unique identifier.
    /// </summary>
    /// <param name="id">The identity ID.</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        Identity? identity = await _db.Identities
            .Include(x => x.MinecraftAccount)
            .Include(x => x.DiscordAccount)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (identity == null)
        {
            return NotFound();
        }

        return Ok(identity);
    }
}
