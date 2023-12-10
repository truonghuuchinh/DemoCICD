using AutoMapper;
using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Domain.Abstractions.Repositories;

namespace DemoCICD.Application.UserCases.V2.Queries.Product;
internal sealed class GetProductByIdHandler : IQueryHandler<Query.GetProductByIdQuery, Response.ProductResponse>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> repositoryBase;
    private readonly IMapper mapper;

    public GetProductByIdHandler(IRepositoryBase<Domain.Entities.Product, Guid> repositoryBase, IMapper mapper)
    {
        this.repositoryBase = repositoryBase;
        this.mapper = mapper;
    }

    public async Task<Result<Response.ProductResponse>> Handle(Query.GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await repositoryBase.FindByIdAsync(request.Id);
        var result = mapper.Map<Response.ProductResponse>(product);
        return result;
    }
}
