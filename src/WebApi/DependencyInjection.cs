using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.WebApi.HealthChecks;
using SSW.CleanArchitecture.WebApi.Services;

namespace SSW.CleanArchitecture.WebApi;
// TODO: Can we remove this?
// #pragma warning disable IDE0055

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddOpenApi();

        services.AddHealthChecks(config);
    }
}
// #pragma warning restore IDE0055