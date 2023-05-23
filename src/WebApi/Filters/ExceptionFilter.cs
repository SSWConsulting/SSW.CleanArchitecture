using Microsoft.AspNetCore.Diagnostics;
using SSW.CleanArchitecture.Application.Common.Exceptions;

namespace SSW.CleanArchitecture.WebApi.Filters;

public static class ExceptionFilter
{
    private static readonly IDictionary<Type, Func<HttpContext, Exception, IResult>> ExceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, IResult>>
    {
        { typeof(ValidationException), HandleValidationException },
        { typeof(NotFoundException), HandleNotFoundException }
    };

    public static void UseExceptionFilter(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp
            => exceptionHandlerApp.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error!;

                await context
                    .HandleException(exception)
                    .ExecuteAsync(context);
            }));
    }


    private static IResult HandleException(this HttpContext context, Exception exception)
    {
        var type = exception.GetType();

        if (ExceptionHandlers.ContainsKey(type))
            return ExceptionHandlers[type].Invoke(context, exception);

        // TODO: Testing around unhandled exceptions (https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/80)
        return Results.Problem(statusCode: StatusCodes.Status500InternalServerError,
            type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
    }

    private static IResult HandleValidationException(HttpContext context, Exception exception)
    {
        var validationException = exception as ValidationException ?? throw new InvalidOperationException("Exception is not of type ValidationException");

        return Results.ValidationProblem(validationException.Errors,
            type: "https://tools.ietf.org/html/rfc7231#section-6.5.1");
    }

    private static IResult HandleNotFoundException(this HttpContext context, Exception exception) =>
        Results.Problem(statusCode: StatusCodes.Status404NotFound,
            title: "The specified resource was not found.",
            type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            detail: exception.Message);
}