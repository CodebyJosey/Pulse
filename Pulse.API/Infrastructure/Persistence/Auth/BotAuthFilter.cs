using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Pulse.API.Domain.Bots;
using Pulse.API.Infrastructure.Persistence;

namespace Pulse.API.Infrastructure.Auth;

public sealed class BotAuthFilter : IAsyncActionFilter
{
    private readonly PulseDbContext _db;
    private readonly bool _requireGuildOwnership;

    public BotAuthFilter(PulseDbContext db, bool requireGuildOwnership)
    {
        _db = db;
        _requireGuildOwnership = requireGuildOwnership;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-BOT-KEY", out var apiKeyValues))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        string apiKey = apiKeyValues.ToString();
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        string hash = BotKeyHasher.Hash(apiKey);

        BotAgent? bot = await _db.BotAgents
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.ApiKeyHash == hash && b.Active);

        if (bot is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Optioneel: guildId moet geclaimed zijn door dezelfde company
        if (_requireGuildOwnership)
        {
            if (!context.RouteData.Values.TryGetValue("guildId", out var guildIdObj))
            {
                context.Result = new BadRequestObjectResult("Missing route parameter: guildId");
                return;
            }

            string guildId = guildIdObj!.ToString()!;
            bool owned = await _db.GuildConnections
                .AsNoTracking()
                .AnyAsync(g => g.GuildId == guildId && g.CompanyId == bot.CompanyId);

            if (!owned)
            {
                context.Result = new ForbidResult();
                return;
            }
        }

        context.HttpContext.Items["CompanyId"] = bot.CompanyId;
        context.HttpContext.Items["BotAgentId"] = bot.Id;

        await next();
    }
}
