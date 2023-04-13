using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.WebApi.Services;

namespace CleanArchitecture.WebApi;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddOpenApiDocument(configure => configure.Title = "CleanArchitecture API");

        services.AddEndpointsApiExplorer();
    }
}