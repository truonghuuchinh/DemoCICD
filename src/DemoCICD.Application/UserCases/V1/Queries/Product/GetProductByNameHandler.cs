using AutoMapper;
using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Services.Product;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public sealed class GetProductByNameHandler : IQueryHandler<Query.GetProductByNameQuery, IEnumerable<Response.ProductResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> repositoryBase;
    private readonly IMapper mapper;

    public GetProductByNameHandler(IRepositoryBase<Domain.Entities.Product, Guid> repositoryBase, IMapper mapper)
    {
        this.repositoryBase = repositoryBase;
        this.mapper = mapper;
    }

    public async Task<Result<IEnumerable<Response.ProductResponse>>> Handle(Query.GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var product = await repositoryBase.FindAll(p => p.Name.ToLower() == request.Name.ToLower()).ToListAsync(cancellationToken);
        var result = mapper.Map<List<Response.ProductResponse>>(product);
        return result;
    }
}
