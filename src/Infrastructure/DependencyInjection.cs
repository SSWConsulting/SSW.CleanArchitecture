using Application.Common.Interfaces;
using Ardalis.Specification;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<EntitySaveChangesInterceptor>();
        services.AddScoped<DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"), builder =>
            {
                builder.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                builder.EnableRetryOnFailure();
            }));

        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddScoped(typeof(IRepositoryBase<>), typeof(DbSetRepository<>));
        services.AddScoped(typeof(IReadRepositoryBase<>), typeof(DbSetRepository<>));

        services.AddSingleton<IDateTime, DateTimeService>();

        return services;
    }
}