using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.V2;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using MediatR;
using static DemoCICD.Contract.Services.V2.Product.Command;
using static DemoCICD.Domain.Exceptions.ProductException;
using ProductEntity = DemoCICD.Domain.Entities.Product;

namespace DemoCICD.Application.UserCases.V2.Commands.Product;
internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdatedProductCommand>
{
    private readonly IRepositoryBase<ProductEntity, Guid> repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPublisher _publisher;

    public UpdateProductCommandHandler(
        IRepositoryBase<ProductEntity, Guid> repository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> Handle(UpdatedProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.FindByIdAsync(request.Id) ?? throw new ProductNotFoundException(request.Id);

        // Update product
        product.Description = request.Description;
        product.Name = request.Name;
        product.Price = request.Price;
        repository.Update(product);

        // Save to database
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Send Email to user when create product success
        await _publisher.Publish(new DomainEvent.ProductUpdated(product!.Id), cancellationToken);
        return Result.Success();
    }
}
