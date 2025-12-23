using Discord;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;
using Pulse.Discord.Services;

namespace Pulse.Discord.Guards;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ModuleEnabledGuard : PreconditionAttribute
{
    private readonly string _moduleKey;

    public ModuleEnabledGuard(string moduleKey)
    {
        _moduleKey = moduleKey;
    }

    public override async Task<PreconditionResult> CheckRequirementsAsync(
        IInteractionContext context,
        ICommandInfo command,
        IServiceProvider services
    )
    {
        if (context.Guild is null)
        {
            return PreconditionResult.FromError("Deze command werkt alleen in servers.");
        }

        ModuleStateService? modules = services.GetRequiredService<ModuleStateService>();
        bool enabled = await modules.IsEnabledAsync(context.Guild.Id, _moduleKey);

        return enabled
            ? PreconditionResult.FromSuccess()
            : PreconditionResult.FromError($"Module `{_moduleKey}` is disabled!");
    }
}