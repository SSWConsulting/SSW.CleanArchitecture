using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using SSW.CleanArchitecture.Infrastructure.Persistence.Interceptors;

namespace SSW.CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    // public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    // {
    //     services.AddScoped<EntitySaveChangesInterceptor>();
    //     services.AddScoped<DispatchDomainEventsInterceptor>();
    //
    //     services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
    //     {
    //         options.AddInterceptors(
    //             services.BuildServiceProvider().GetRequiredService<EntitySaveChangesInterceptor>(),
    //             services.BuildServiceProvider().GetRequiredService<DispatchDomainEventsInterceptor>()
    //         );
    //
    //         options.UseSqlServer(config.GetConnectionString("DefaultConnection"), builder =>
    //         {
    //             builder.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
    //             builder.EnableRetryOnFailure();
    //         });
    //     });
    //
    //     services.AddSingleton(TimeProvider.System);
    //
    //     return services;
    // }

    public static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<ApplicationDbContext>("clean-architecture",
            null,
            options =>
            {
                var serviceProvider = builder.Services.BuildServiceProvider();
                options.AddInterceptors(
                    serviceProvider.GetRequiredService<EntitySaveChangesInterceptor>(),
                    serviceProvider.GetRequiredService<DispatchDomainEventsInterceptor>());
                // TODO: Add this
                // options.UseExceptionProcessor();
            });

        builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<EntitySaveChangesInterceptor>();
        builder.Services.AddScoped<DispatchDomainEventsInterceptor>();

        builder.Services.AddSingleton(TimeProvider.System);
    }
}