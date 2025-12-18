using Microsoft.AspNetCore.Mvc;
using Pulse.API.Domain.Bots;
using Pulse.API.Domain.Companies;
using Pulse.API.Infrastructure.Auth;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly PulseDbContext _db;
    public CompaniesController(PulseDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string name)
    {
        Company? company = new Company { Id = Guid.NewGuid(), Name = name };
        string apiKey = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");

        BotAgent bot = new BotAgent
        {
            Id = Guid.NewGuid(),
            CompanyId = company.Id,
            ApiKeyHash = BotKeyHasher.Hash(apiKey)
        };

        _db.Companies.Add(company);
        _db.BotAgents.Add(bot);
        await _db.SaveChangesAsync();

        return Ok(new
        {
            companyId = company.Id,
            botApiKey = apiKey // 1x tonen
        });
    }
}
