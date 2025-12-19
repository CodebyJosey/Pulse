using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pulse.API.Domain.Bots;
using Pulse.API.Domain.Companies;
using Pulse.API.Infrastructure.Auth;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Controllers;

[ApiController]
[Route("api/companies")]
[Authorize]
public class CompaniesController : ControllerBase
{
    private readonly PulseDbContext _db;
    public CompaniesController(PulseDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] string name)
    {
        string? userId =
            User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var company = new Company
        {
            Name = name,
            OwnerUserId = Guid.Parse(userId)
        };

        _db.Companies.Add(company);

        // 🔑 Bot + API key aanmaken hoort HIER
        string apiKey = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");

        BotAgent? bot = new BotAgent
        {
            CompanyId = company.Id,
            ApiKeyHash = BotKeyHasher.Hash(apiKey)
        };

        _db.BotAgents.Add(bot);

        await _db.SaveChangesAsync();

        return Ok(new
        {
            companyId = company.Id,
            botApiKey = apiKey // 1x tonen
        });
    }
}
