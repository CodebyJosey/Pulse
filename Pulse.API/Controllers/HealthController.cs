using Microsoft.AspNetCore.Mvc;

namespace Pulse.API;

/// <summary>
/// Provides health and status information for the Pulse API.
/// </summary>
[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Returns the current health status of the Pulse API.
    /// </summary>
    /// <remarks>
    /// This endpoint can be used for uptime monotoring,
    /// load balancers and basic connectivity checks.
    /// </remarks>
    /// <returns>
    /// A simple object indicating that the API is running.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "ok",
            service = "Pulse API",
            timestamp = DateTime.UtcNow
        });
    }
}