using MediatR;

namespace DemoCICD.Contract.Abstractions.Messages;
public interface IDomainEventHandler<in TEvent>
    : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
