using Microsoft.AspNetCore.Mvc;
using Pulse.API.Application.Logging;
using Pulse.API.Contracts.Logging;
using Pulse.API.Domain.Logging;
using Pulse.API.Infrastructure.Auth;

namespace Pulse.API.Controllers;

[ApiController]
[Route("api/company/logs")]
[BotAuth(requireGuildOwnership: false)]
public class CompanyLogsController : ControllerBase
{
    private readonly ICompanyLogService _companyLogs;

    public CompanyLogsController(ICompanyLogService companyLogs)
    {
        _companyLogs = companyLogs;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCompanyLogRequest request
    )
    {
        if(!HttpContext.Items.TryGetValue("CompanyId", out object? companyIdObj) || companyIdObj is not Guid companyId)
        {
            return Unauthorized();
        }

        CompanyLog log = new CompanyLog
        {
            Id = Guid.NewGuid(),
            Timestamp = DateTimeOffset.UtcNow,
            CompanyId = companyId,
            GuildId = request.GuildId,
            ModuleKey = request.ModuleKey,
            EventType = request.EventType,
            Message = request.Message,
            MetadataJson = request.MetadataJson
        };

        await _companyLogs.LogAsync(log);
        return NoContent();
    }
}