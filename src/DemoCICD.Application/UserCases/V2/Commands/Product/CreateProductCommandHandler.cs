using AutoMapper;
using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.V2;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Exceptions;
using MediatR;
using static DemoCICD.Contract.Services.V2.Product.Command;
using ProductEntity = DemoCICD.Domain.Entities.Product;

namespace DemoCICD.Application.UserCases.V2.Commands.Product;
internal sealed class CreateProductCommandHandler : ICommandHandler<CreatedProductCommand>
{
    private readonly IRepositoryBase<ProductEntity, Guid> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public CreateProductCommandHandler(
        IRepositoryBase<ProductEntity, Guid> repository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> Handle(CreatedProductCommand request, CancellationToken cancellationToken)
    {
        // Check name must be unique
        var product = await _repository.FindSingleAsync(x => x.Name.ToLower() == request.Name!.ToLower());
        if (product is not null)
        {
            throw new BadRequestException("Can't dupplicate name of product");
        }

        // Create product
        var productMapper = _mapper.Map<ProductEntity>(request);
        _repository.Add(productMapper);

        // Save to database handle in Global transaction to make sure
        // Every data is consistent
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Send Email to user when create product success
        await _publisher.Publish(new DomainEvent.ProductCreated(productMapper!.Id), cancellationToken);

        return Result.Success();
    }
}
