using Microsoft.AspNetCore.Mvc;
using Pulse.API.Common.Errors;
using Pulse.API.Contracts.Requests;
using Pulse.API.Domain.Events;

namespace Pulse.API.Controllers;

/// <summary>
/// API endpoints for Pulse events. 
/// </summary>
[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly EventService _eventService;
    private readonly EventQueryService _queryService;

    public EventsController(
        EventService eventService,
        EventQueryService queryService
    )
    {
        _eventService = eventService;
        _queryService = queryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int limit = 100
    )
    {
        IReadOnlyList<PulseEvent>? events = await _queryService.GetAllAsync(limit);
        return Ok(events);
    }

    [HttpGet("~/api/companies/{companyId:guid}/events")]
    public async Task<IActionResult> GetByCompany(
        Guid companyId,
        [FromQuery] EventPlatform? platform,
        [FromQuery] PulseEventType? type,
        [FromQuery] int limit = 100
    )
    {
        IReadOnlyList<PulseEvent>? events =
        await _queryService.GetByCompanyAsync(
            companyId,
            platform,
            type,
            limit
        );

        return Ok(events);
    }

    /// <summary>
    /// Creates a new Pulse event, based on information.
    /// </summary>
    /// <param name="request">The complete request to process.</param>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePulseEventRequest request
    )
    {
        Validate(request);

        PulseEvent createdEvent = await _eventService.CreateAsync(
            request.CompanyId,
            request.IdentityId,
            request.Platform,
            request.Type,
            request.OccurredAt,
            request.Payload
        );

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("minecraft/player-join")]
    public Task<IActionResult> MinecraftPlayerJoin(
        [FromBody] CreatePulseEventRequest request
    )
    {
        request.Platform = EventPlatform.Minecraft;
        request.Type = PulseEventType.PlayerJoin;
        return Create(request);
    }

    [HttpPost("minecraft/player-leave")]
    public Task<IActionResult> MinecraftPlayerLeave(
        [FromBody] CreatePulseEventRequest request
    )
    {
        request.Platform = EventPlatform.Minecraft;
        request.Type = PulseEventType.PlayerLeave;
        return Create(request);
    }

    [HttpPost("minecraft/command")]
    public Task<IActionResult> MinecraftCommand(
        [FromBody] CreatePulseEventRequest request
    )
    {
        request.Platform = EventPlatform.Minecraft;
        request.Type = PulseEventType.CommandExecuted;
        return Create(request);
    }

    [HttpPost("discord/message")]
    public Task<IActionResult> DiscordMessage(
        [FromBody] CreatePulseEventRequest request
    )
    {
        request.Platform = EventPlatform.Discord;
        request.Type = PulseEventType.MessageSent;
        return Create(request);
    }

    [HttpPost("discord/command")]
    public Task<IActionResult> DiscordCommand(
        [FromBody] CreatePulseEventRequest request
    )
    {
        request.Platform = EventPlatform.Discord;
        request.Type = PulseEventType.CommandExecuted;
        return Create(request);
    }
    
    private static void Validate(CreatePulseEventRequest request)
    {
        if (request.CompanyId == Guid.Empty)
        {
            throw new ValidationException("CompanyId is required.");
        }
        if (request.OccurredAt == default)
        {
            throw new ValidationException("OccurredAt is required.");
        }
        if (string.IsNullOrWhiteSpace(request.Payload))
        {
            throw new ValidationException("Payload is required.");
        }
    }
}