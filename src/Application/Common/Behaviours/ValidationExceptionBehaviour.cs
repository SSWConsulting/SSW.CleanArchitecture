namespace SSW.CleanArchitecture.Application.Common.Behaviours;

public class ValidationExceptionBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Count is not 0)
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count is not 0)
                throw new Exceptions.ValidationException(failures);
        }
        return await next();
    }
}