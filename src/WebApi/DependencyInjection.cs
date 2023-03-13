using Application.Common.Interfaces;
using Infrastructure.Persistence;
using WebApi.Services;

namespace WebApi;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();
    }
}