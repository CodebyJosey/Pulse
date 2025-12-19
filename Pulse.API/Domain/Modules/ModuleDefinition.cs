namespace Pulse.API.Domain.Modules;

public class ModuleDefinition
{
    public Guid Id { get; set; }
    public string Key { get; set; } = null!; // logging / moderation / ...
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}