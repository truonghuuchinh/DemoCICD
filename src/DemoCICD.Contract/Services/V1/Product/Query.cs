using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Enumerations;
using static DemoCICD.Contract.Services.V1.Product.Response;

namespace DemoCICD.Contract.Services.V1.Product;
public static class Query
{
    public sealed record GetProductQuery(
        string? SearchTerm,
        string? SortColumn,
        SortOrder? SortOrder,
        IDictionary<string, SortOrder>? SortColumnAndOrder,
        int PageIndex,
        int PageSize)
        : IQuery<PagedResult<ProductResponse>>;

    public sealed record GetProductByIdQuery(Guid Id)
        : IQuery<ProductResponse>;
}
