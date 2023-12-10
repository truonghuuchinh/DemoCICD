using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.V1;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using MediatR;
using static DemoCICD.Contract.Services.V1.Product.Command;
using static DemoCICD.Domain.Exceptions.ProductException;
using ProductEntity = DemoCICD.Domain.Entities.Product;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
internal sealed class DeleteProductCommandHandler : ICommandHandler<DeletedProductCommand>
{
    private readonly IRepositoryBase<ProductEntity, Guid> repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPublisher _publisher;

    public DeleteProductCommandHandler(
        IRepositoryBase<ProductEntity, Guid> repository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> Handle(DeletedProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.FindByIdAsync(request.Id) ?? throw new ProductNotFoundException(request.Id);

        // Delete product
        repository.Remove(product);

        // Save to database
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Publish event to send email service
        await _publisher.Publish(new DomainEvent.ProductDeleted(product.Id), cancellationToken);
        return Result.Success();
    }
}
