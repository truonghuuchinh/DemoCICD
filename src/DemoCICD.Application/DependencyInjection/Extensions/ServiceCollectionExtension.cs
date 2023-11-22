using DemoCICD.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCICD.Application.DependencyInjection.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddConfigurationMediaR();
        return services;
    }

    public static IServiceCollection AddConfigurationMediaR(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(AssemblyReference.Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(Contract.AssemblyReference.Assembly, includeInternalTypes: true);
        return services;
    }
}
