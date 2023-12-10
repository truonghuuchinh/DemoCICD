using DemoCICD.Contract.Abstractions.Messages;

namespace DemoCICD.Contract.Services.V2;
public static class DomainEvent
{
    public record ProductCreated(Guid Id)
        : IDomainEvent;

    public record ProductDeleted(Guid Id)
        : IDomainEvent;

    public record ProductUpdated(Guid Id)
        : IDomainEvent;
}
