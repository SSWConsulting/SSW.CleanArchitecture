using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Infrastructure.Audit;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using SSW.CleanArchitecture.Infrastructure.Persistence.Interceptors;

namespace SSW.CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<AuditEntityInterceptor>();
        services.AddScoped<DispatchDomainEventsInterceptor>();

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"), builder =>
            {
                builder.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                builder.EnableRetryOnFailure();
            }));

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddDbContext<AuditDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"), builder =>
            {
                builder.MigrationsHistoryTable("__EFMigrationsHistory", "audit");
                builder.EnableRetryOnFailure();
            }));

        services.AddScoped<AuditDbContextInitializer>();

        services.AddSingleton(TimeProvider.System);

        return services;
    }
}