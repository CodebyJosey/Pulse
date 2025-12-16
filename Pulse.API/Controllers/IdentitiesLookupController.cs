using Microsoft.AspNetCore.Mvc;
using Pulse.API.Domain.Identities;
using Pulse.API.Services;

namespace Pulse.API.Controllers;

/// <summary>
/// Provides lookup endpoints for identities.
/// </summary>
[ApiController]
[Route("api/identities/lookup")]
public class IdentityLookupController : ControllerBase
{
    private readonly IdentityLookupService _lookup;

    public IdentityLookupController(IdentityLookupService lookup)
    {
        _lookup = lookup;
    }

    /// <summary>
    /// Finds an identity by Minecraft username.
    /// </summary>
    [HttpGet("minecraft")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindByMinecraft([FromQuery] string username)
    {
        Identity? identity = await _lookup.FindByMinecraftUsernameAsync(username);

        if (identity == null)
        {
            return NotFound();
        }

        return Ok(identity);
    }

    /// <summary>
    /// Finds an identity by Discord user ID.
    /// </summary>
    [HttpGet("discord")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindByDiscord([FromQuery] ulong discordId)
    {
        Identity? identity = await _lookup.FindByDiscordIdAsync(discordId);

        if (identity == null)
        {
            return NotFound();
        }

        return Ok(identity);
    }
}
