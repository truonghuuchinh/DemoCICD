using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Shared;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, GetProductQueryResponse>
{
    public Task<Result<GetProductQueryResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
