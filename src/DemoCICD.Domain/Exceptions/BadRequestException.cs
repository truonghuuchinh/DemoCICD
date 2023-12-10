namespace DemoCICD.Domain.Exceptions;

public sealed class BadRequestException : DomainException
{
    public BadRequestException(string message)
        : base("Bad Request", message)
    {
    }
}
