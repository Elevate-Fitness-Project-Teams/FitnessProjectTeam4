using FluentValidation;
using MediatR;
using ProgressService.Common.Responses;
using System.Reflection;

namespace ProgressService.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
          : IPipelineBehavior<TRequest, TResponse>
          where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (!validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, ct)));
            var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                var errorMessage = failures.FirstOrDefault()?.ErrorMessage;

                var resultType = typeof(TResponse).GetGenericArguments()[0];
                var genericResultType = typeof(RequestResult<>).MakeGenericType(resultType);

                var failureMethod = genericResultType.GetMethod("Failure",
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    new[] { typeof(ErrorCode), typeof(string) },
                    null);

                if (failureMethod != null)
                {
                    var result = failureMethod.Invoke(null, new object[] { ErrorCode.ValidationError, errorMessage! });
                    return (TResponse)result!;
                }

                throw new InvalidOperationException($"Architecture Error: 'Failure' method with message not found in {genericResultType.Name}");
            }

            return await next();
        }
    }
}
