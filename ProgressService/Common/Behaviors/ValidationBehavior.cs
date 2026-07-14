using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProgressService.Common.Responses;
using System.Reflection;

namespace ProgressService.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(
     IEnumerable<IValidator<TRequest>> validators)
     : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            ValidationResult[] validationResults =
                await Task.WhenAll(
                    validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var errors = validationResults
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .Select(x => x.ErrorMessage)
                .Distinct()
                .ToList();

            if (!errors.Any())
                return await next();

            Type[] genericArguments = typeof(TResponse).GetGenericArguments();

            if (genericArguments.Length != 1)
                throw new InvalidOperationException($"{typeof(TResponse).Name} must be of type RequestResult<T>.");

            var resultType = genericArguments[0];

            var requestResultType = typeof(RequestResult<>).MakeGenericType(resultType);

            var validationFailureMethod = requestResultType.GetMethod(
                nameof(RequestResult<object>.ValidationFailure),
                BindingFlags.Public | BindingFlags.Static);

            if (validationFailureMethod is null)
                throw new InvalidOperationException($"ValidationFailure method was not found on {requestResultType.Name}.");

            var response = validationFailureMethod.Invoke(null, new object[] { errors });

            return (TResponse)response!;
        }
    }
}
