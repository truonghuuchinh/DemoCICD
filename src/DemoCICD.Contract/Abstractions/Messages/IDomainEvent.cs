using MediatR;

namespace DemoCICD.Contract.Abstractions.Messages;
public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
