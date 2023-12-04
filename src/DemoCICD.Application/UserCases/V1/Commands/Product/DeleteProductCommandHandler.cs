using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using static DemoCICD.Contract.Abstractions.Services.Product.Command;
using static DemoCICD.Domain.Exceptions.ProductException;
using ProductEntity = DemoCICD.Domain.Entities.Product;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class DeleteProductCommandHandler : ICommandHandler<DeletedProduct>
{
    private readonly IRepositoryBase<ProductEntity, Guid> repository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteProductCommandHandler(
        IRepositoryBase<ProductEntity, Guid> repository,
        IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeletedProduct request, CancellationToken cancellationToken)
    {
        var product = await repository.FindByIdAsync(request.Id) ?? throw new ProductNotFoundException(request.Id);

        // Delete product
        repository.Remove(product);

        // Save to database
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
