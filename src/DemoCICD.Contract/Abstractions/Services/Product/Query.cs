using DemoCICD.Contract.Abstractions.Messages;
using static DemoCICD.Contract.Abstractions.Services.Product.Response;

namespace DemoCICD.Contract.Abstractions.Services.Product;
public static class Query
{
    public record GetProductQuery() : IQuery<IEnumerable<ProductResponse>>;

    public record GetProductByNameQuery(string Name) : IQuery<IEnumerable<ProductResponse>>;

    public record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;
}
