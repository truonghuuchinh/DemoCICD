using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Domain.Entities;
using static DemoCICD.Contract.Services.V1.Product.Command;
using static DemoCICD.Contract.Services.V1.Product.Response;
using CommandV2 = DemoCICD.Contract.Services.V2.Product.Command;
using ResponseV2 = DemoCICD.Contract.Services.V2.Product.Response;

namespace DemoCICD.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<PagedResult<Product>, PagedResult<ProductResponse>>();
        CreateMap<Product, ProductResponse>();
        CreateMap<CreatedProductCommand, Product>();

        CreateMap<PagedResult<Product>, PagedResult<ResponseV2.ProductResponse>>();
        CreateMap<Product, ResponseV2.ProductResponse>();
        CreateMap<CommandV2.CreatedProductCommand, Product>();
    }
}
