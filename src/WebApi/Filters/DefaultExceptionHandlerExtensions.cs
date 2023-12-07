namespace SSW.CleanArchitecture.WebApi.Filters;

public static class DefaultExceptionHandlerExtensions
{
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