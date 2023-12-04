using AutoMapper;
using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Services.Product;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public sealed class GetProductQueryHandler : IQueryHandler<Query.GetProductQuery, IEnumerable<Response.ProductResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> repositoryBase;
    private readonly IMapper mapper;

    public GetProductQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> repositoryBase, IMapper mapper)
    {
        this.repositoryBase = repositoryBase;
        this.mapper = mapper;
    }

    public async Task<Result<IEnumerable<Response.ProductResponse>>> Handle(Query.GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await repositoryBase.FindAll().ToListAsync();
        var result = mapper.Map<List<Response.ProductResponse>>(products);
        return result;
    }
}
