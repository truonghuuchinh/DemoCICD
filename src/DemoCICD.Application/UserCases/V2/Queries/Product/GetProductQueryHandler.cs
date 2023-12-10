using System.Globalization;
using System.Linq.Expressions;
using AutoMapper;
using DemoCICD.Contract.Abstractions.Messages;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Persistence;
using Microsoft.EntityFrameworkCore;
using ProductEntity = DemoCICD.Domain.Entities.Product;

namespace DemoCICD.Application.UserCases.V2.Queries.Product;
internal sealed class GetProductQueryHandler : IQueryHandler<Query.GetProductQuery, PagedResult<Response.ProductResponse>>
{
    private readonly IRepositoryBase<ProductEntity, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public GetProductQueryHandler(
        IRepositoryBase<ProductEntity, Guid> repositoryBase,
        IMapper mapper,
        ApplicationDbContext context)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<PagedResult<Response.ProductResponse>>> Handle(Query.GetProductQuery request, CancellationToken cancellationToken)
    {
        PagedResult<Response.ProductResponse>? result = null;
        if (request.SortColumnAndOrder?.Count > 0)
        {
            var (pageIndex, pageSize) = CheckPaginatioInfor(request);

            var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                ? @$"SELECT * FROM {nameof(ProductEntity)} ORDER BY "
                : @$"SELECT * FROM {nameof(ProductEntity)}
                        WHERE {nameof(ProductEntity.Name)} LIKE '%{request.SearchTerm}%'
                        OR {nameof(ProductEntity.Description)} LIKE '%{request.SearchTerm}%'
                        ORDER BY ";

            foreach (var item in request.SortColumnAndOrder)
            {
                productsQuery += item.Value == SortOrder.Descending
                ? $"{item.Key} DESC, "
                : $"{item.Key} ASC, ";
            }

            productsQuery = productsQuery.Remove(productsQuery.Length - 2);

            productsQuery += $" OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            var products = _context.Products.FromSqlRaw(productsQuery);

            var productPagedResult = await PagedResult<ProductEntity>
                .CreateAsync(products, pageIndex, pageSize);

            result = _mapper.Map<PagedResult<Response.ProductResponse>>(productPagedResult);
        }
        else
        {
            var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
            ? _repositoryBase.FindAll()
            : _repositoryBase.FindAll(p =>
                    p.Name.Contains(request.SearchTerm) ||
                    p.Description.Contains(request.SearchTerm));

            var keySelector = ConvertKeySelector(request);

            productsQuery = request.SortOrder == SortOrder.Descending
                ? productsQuery.OrderByDescending(keySelector)
                : productsQuery.OrderBy(keySelector);

            var products = await PagedResult<ProductEntity>.CreateAsync(productsQuery, request.PageIndex, request.PageSize);
            result = _mapper.Map<PagedResult<Response.ProductResponse>>(products);
        }

        return result;
    }

    private static Expression<Func<ProductEntity, object>> ConvertKeySelector(Query.GetProductQuery request)
    {
        Expression<Func<ProductEntity, object>> keySelector = request.SortColumn?.ToLower(CultureInfo.CurrentCulture) switch
        {
            "name" => product => product.Name,
            "price" => product => product.Price,
            "description" => product => product.Description,
            _ => product => product.Id,

            // _ => product => product.CreatedDate,
        };
        return keySelector;
    }

    private static (int, int) CheckPaginatioInfor(Query.GetProductQuery request)
    {
        var pageIndex = request.PageIndex <= 0 ? PagedResult<ProductEntity>.DefaultPageIndex : request.PageIndex;
        var pageSize = request.PageSize <= 0
            ? PagedResult<ProductEntity>.DefaultPageSize
            : request.PageSize > PagedResult<ProductEntity>.UpperPageSize
            ? PagedResult<ProductEntity>.UpperPageSize : request.PageSize;

        return (pageIndex, pageSize);
    }
}
