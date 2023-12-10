using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Services.V1;

namespace DemoCICD.Application.UserCases.V1.EventHandlers;

public class SendSmsWhenProductChangedEventHandler
    : IDomainEventHandler<DomainEvent.ProductCreated>,
    IDomainEventHandler<DomainEvent.ProductDeleted>,
    IDomainEventHandler<DomainEvent.ProductUpdated>
{
    public async Task Handle(DomainEvent.ProductCreated notification, CancellationToken cancellationToken)
    {
        await SendSms();
        await Task.Delay(5000);
    }

    public async Task Handle(DomainEvent.ProductDeleted notification, CancellationToken cancellationToken)
    {
        await SendSms();
        await Task.Delay(5000);
    }

    public async Task Handle(DomainEvent.ProductUpdated notification, CancellationToken cancellationToken)
    {
        await SendSms();
    }

    private async Task SendSms()
    {
        await Task.CompletedTask;
    }
}
