using ProgressService.Common.Exceptions;
using ProgressService.Common.Responses;

namespace ProgressService.Middlewares
{
    public class GlobalErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = StatusCodes.Status500InternalServerError;

            ErrorCode errorCode = ErrorCode.InternalServerError;

            switch (exception)
            {
                case DomainException domainException:

                    errorCode = domainException.ErrorCode;

                    statusCode = domainException.ErrorCode switch
                    {
                        ErrorCode.ValidationError => StatusCodes.Status400BadRequest,

                        ErrorCode.NullValue => StatusCodes.Status400BadRequest,

                        ErrorCode.UnAuthorized => StatusCodes.Status401Unauthorized,

                        ErrorCode.Forbidden => StatusCodes.Status403Forbidden,

                        _ => StatusCodes.Status400BadRequest
                    };

                    break;

                default:

                    errorCode = ErrorCode.InternalServerError;

                    statusCode = StatusCodes.Status500InternalServerError;

                    break;
            }

            context.Response.StatusCode = statusCode;

            context.Response.ContentType = "application/json";

            var response = new FailedResponseViewModel(errorCode, DateTime.UtcNow);

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
