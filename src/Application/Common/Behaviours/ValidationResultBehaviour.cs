// Original source: https://github.com/NimblePros/MediatR.Contrib/blob/main/src/NimblePros.MediatR.Contrib/Behaviors/ValidationBehavior.cs
using Ardalis.Result;
using Ardalis.Result.FluentValidation;

namespace SSW.CleanArchitecture.Application.Common.Behaviours;

/// <summary>
/// This behavior assumes validators have been registered with the container.
/// Example:
/// builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
///        .Where(t => t.IsClosedTypeOf(typeof(IValidator&lt;&gt;)))
///        .AsImplementedInterfaces();
///
/// You'll also need to register this behavior:
/// Example:
/// builder.RegisterGeneric(typeof(ValidationBehavior&lt;,&gt;))
///     .As(typeof(IPipelineBehavior&lt;,&gt;));
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidationResultBehavior<TRequest, TResponse> :
  IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public ValidationResultBehavior(IEnumerable<IValidator<TRequest>> validators)
  {
    _validators = validators;
  }

  public async Task<TResponse> Handle(TRequest request,
    RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    if (_validators.Any())
    {
      var context = new ValidationContext<TRequest>(request);

      var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
      var resultErrors = validationResults.SelectMany(r => r.AsErrors()).ToList();
      var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

#nullable disable
      if (failures.Count != 0)
      {
        if (typeof(TResponse).IsGenericType &&
          typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
          var resultType = typeof(TResponse).GetGenericArguments()[0];
          var invalidMethod = typeof(Result<>)
              .MakeGenericType(resultType)
              .GetMethod(nameof(Result<int>.Invalid), new[] { typeof(List<ValidationError>) });

          if (invalidMethod != null)
          {
            return (TResponse)invalidMethod.Invoke(null, new object[] { resultErrors });
          }
        }
        else if (typeof(TResponse) == typeof(Result))
        {
          return (TResponse)(object)Result.Invalid(resultErrors);
        }
        else
        {
          throw new ValidationException(failures);
        }
      }
#nullable enable
    }
    return await next();
  }
}