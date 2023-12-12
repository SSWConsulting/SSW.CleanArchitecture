namespace SSW.CleanArchitecture.WebApi.Filters;

public static class DefaultExceptionHandlerExtensions
{
    // NOTE: Usually app.UseExceptionHandler() is used in conjunction with IExceptionHandler, but this also requires the ProblemDetails service to be registered so that it can be used for unhandled exceptions.  This is not needed as we will handle all exceptions either via this middleware or KnownExceptionsHandler.
    public static void UseDefaultExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp
            => exceptionHandlerApp.Run(async context =>
            {
                IResult defaultExceptionResponse = Results.Problem(statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");

                await defaultExceptionResponse.ExecuteAsync(context);
            }));
    }
}