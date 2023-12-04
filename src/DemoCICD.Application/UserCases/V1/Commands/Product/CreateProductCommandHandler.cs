using AutoMapper;
using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Exceptions;
using static DemoCICD.Contract.Abstractions.Services.Product.Command;
using ProductEntity = DemoCICD.Domain.Entities.Product;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class CreateProductCommandHandler : ICommandHandler<CreatedProduct>
{
    private readonly IRepositoryBase<ProductEntity, Guid> repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateProductCommandHandler(
        IRepositoryBase<ProductEntity, Guid> repository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreatedProduct request, CancellationToken cancellationToken)
    {
        // Check name must be unique
        var product = await repository.FindSingleAsync(x => x.Name.ToLower() == request.Name!.ToLower());
        if (product is not null)
        {
            throw new BadRequestException("Can't dupplicate name of product");
        }

        // Create product
        var productMapper = mapper.Map<ProductEntity>(request);
        repository.Add(productMapper);

        // Save to database
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
