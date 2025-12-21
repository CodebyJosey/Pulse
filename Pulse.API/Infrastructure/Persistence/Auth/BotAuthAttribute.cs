using Microsoft.AspNetCore.Mvc;

namespace Pulse.API.Infrastructure.Auth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class BotAuthAttribute : TypeFilterAttribute
{
    public BotAuthAttribute(bool requireGuildOwnership = false) : base(typeof(BotAuthFilter))
    {
        Arguments = new object[]
        {
            requireGuildOwnership
        };
    }
}