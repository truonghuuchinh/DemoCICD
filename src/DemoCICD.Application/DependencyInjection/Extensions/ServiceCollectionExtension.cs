﻿using DemoCICD.Application.Behaviors;
using DemoCICD.Application.Mapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCICD.Application.DependencyInjection.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddConfigurationMediaR();
        services.AddConfigurationAutoMapper();
        return services;
    }

    public static IServiceCollection AddConfigurationMediaR(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(AssemblyReference.Assembly));
        services.AddValidatorsFromAssembly(Contract.AssemblyReference.Assembly, includeInternalTypes: true);

        // Add validation pipeline before sending request to handler
        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));

        return services;
    }

    public static IServiceCollection AddConfigurationAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceProfile));
        return services;
    }
}
