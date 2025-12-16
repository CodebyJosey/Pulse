namespace Pulse.API.Common.Errors;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}