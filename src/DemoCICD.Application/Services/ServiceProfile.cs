using AutoMapper;
using DemoCICD.Domain.Entities;
using static DemoCICD.Contract.Abstractions.Services.Product.Command;
using static DemoCICD.Contract.Abstractions.Services.Product.Response;

namespace DemoCICD.Application.Services;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Product, ProductResponse>();
        CreateMap<CreatedProduct, Product>();
    }
}
