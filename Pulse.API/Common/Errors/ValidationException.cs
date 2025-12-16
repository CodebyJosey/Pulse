namespace Pulse.API.Common.Errors;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}