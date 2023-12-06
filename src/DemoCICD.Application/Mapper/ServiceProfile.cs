using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Domain.Entities;
using static DemoCICD.Contract.Abstractions.Services.Product.Command;
using static DemoCICD.Contract.Abstractions.Services.Product.Response;

namespace DemoCICD.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<PagedResult<Product>, PagedResult<ProductResponse>>();
        CreateMap<Product, ProductResponse>();
        CreateMap<CreatedProductCommand, Product>();
    }
}
