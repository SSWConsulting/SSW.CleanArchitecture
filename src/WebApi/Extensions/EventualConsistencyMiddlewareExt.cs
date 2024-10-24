using SSW.CleanArchitecture.Infrastructure.Middleware;

namespace SSW.CleanArchitecture.WebApi.Extensions;

public static class EventualConsistencyMiddlewareExt
{
    public static IApplicationBuilder UseEventualConsistencyMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<EventualConsistencyMiddleware>();
        return app;
    }
}