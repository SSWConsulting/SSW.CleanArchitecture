using Ardalis.Specification;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Persistence.Interceptors;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<EntitySaveChangesInterceptor>();

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