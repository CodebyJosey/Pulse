namespace Pulse.API.Domain.Modules;

public class GuildModuleState
{
    public Guid Id { get; set; }
    public string GuildId { get; set; } = null!;
    public Guid ModuleId { get; set; }
    public bool Enabled { get; set; }
    public ModuleDefinition Module { get; set; } = null!;
}