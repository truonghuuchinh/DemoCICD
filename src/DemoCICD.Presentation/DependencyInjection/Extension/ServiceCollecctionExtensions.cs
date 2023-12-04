using Microsoft.Extensions.DependencyInjection;

namespace DemoCICD.Presentation.DependencyInjection.Extension;
public static class ServiceCollecctionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSwagger();
        services
            .AddApiVersioning(options => options.ReportApiVersions = true)
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        return services;
    }
}
