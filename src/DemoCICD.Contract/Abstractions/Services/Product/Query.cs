using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Enumerations;
using static DemoCICD.Contract.Abstractions.Services.Product.Response;

namespace DemoCICD.Contract.Abstractions.Services.Product;
public static class Query
{
    public record GetProductQuery(
        string? SearchTerm,
        string? SortColumn,
        SortOrder? SortOrder,
        IDictionary<string, SortOrder>? SortColumnAndOrder,
        int PageIndex,
        int PageSize)
        : IQuery<PagedResult<ProductResponse>>;

    public record GetProductByIdQuery(Guid Id)
        : IQuery<ProductResponse>;
}
