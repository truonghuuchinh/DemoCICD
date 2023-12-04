using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Entities.Identity;
using DemoCICD.Persistence.DependencyInjection.Options;
using DemoCICD.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DemoCICD.Persistence.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddConfigurationDbContext();
        services.AddConfigurationRepositoryBase();

        using (var service = services.BuildServiceProvider().CreateScope())
        {
            var context = service.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Seeding();
        }

        return services;
    }

    public static OptionsBuilder<SqlServerRetryOptions> ConfigurationServiceRetryOptions(
        this IServiceCollection services,
        IConfigurationSection section) => services
        .AddOptions<SqlServerRetryOptions>()
        .Bind(section)
        .ValidateDataAnnotations()
        .ValidateOnStart();

    private static IServiceCollection AddConfigurationDbContext(this IServiceCollection services)
    {
        services.AddDbContextPool<ApplicationDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var options = provider.GetRequiredService<IOptionsMonitor<SqlServerRetryOptions>>();

            #region ============== SQL-SERVER-STRATEGY-1 ==============

            builder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true)
            .UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
            .UseSqlServer(
                connectionString: configuration.GetConnectionString("ConnectionStrings"),
                sqlServerOptionsAction: optionsBuilder
                        => optionsBuilder.ExecutionStrategy(
                                dependencies => new SqlServerRetryingExecutionStrategy(
                                    dependencies: dependencies,
                                    maxRetryCount: options.CurrentValue.MaxRetryCount,
                                    maxRetryDelay: options.CurrentValue.MaxRetryDelay,
                                    errorNumbersToAdd: options.CurrentValue.ErrorNumbersToAdd))
                            .MigrationsAssembly(AssemblyReference.Assembly.GetName().Name));

            #endregion

            #region ============== SQL-SERVER-STRATEGY-2 ==============

            //builder
                //.EnableDetailedErrors(true)
                //.EnableSensitiveDataLogging(true)
                //.UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
                //.UseSqlServer(
                //    connectionString: configuration.GetConnectionString("ConnectionStrings"),
                //        sqlServerOptionsAction: optionsBuilder
                //            => optionsBuilder
                //            .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));

            #endregion
        });

        services
            .AddIdentityCore<AppUser>()
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.AllowedForNewUsers = true; // Default true
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2); // Default 5
            options.Lockout.MaxFailedAccessAttempts = 3; // Default 5

            // Setup for service
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        });

        return services;
    }

    private static void AddConfigurationRepositoryBase(this IServiceCollection services)
    {
        services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
        services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
    }
}
