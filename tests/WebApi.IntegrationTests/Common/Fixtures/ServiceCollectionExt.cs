using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SSW.CleanArchitecture.Infrastructure.Persistence.Interceptors;
using System.Diagnostics;

namespace WebApi.IntegrationTests.Common.Fixtures;

internal static class ServiceCollectionExt
{
    /// <summary>
    /// Replaces the DbContext with a new instance using the provided database container
    /// </summary>
    internal static IServiceCollection ReplaceDbContext<T>(
        this IServiceCollection services,
        DatabaseContainer databaseContainer) where T : DbContext
    {
        services
            .RemoveAll<DbContextOptions<T>>()
            .RemoveAll<T>()
            .AddDbContext<T>((_, options) =>
            {
                options.UseSqlServer(databaseContainer.ConnectionString,
                    b => b.MigrationsAssembly(typeof(T).Assembly.FullName));

                options.LogTo(m => Debug.WriteLine(m));
                options.EnableSensitiveDataLogging();

                options.AddInterceptors(
                    services.BuildServiceProvider().GetRequiredService<EntitySaveChangesInterceptor>(),
                    services.BuildServiceProvider().GetRequiredService<DispatchDomainEventsInterceptor>()
                );
            });

        return services;
    }
}