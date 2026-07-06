namespace ProfileService.Common.Middlewares
{
    public class GlobalErrorHandling(ILogger<GlobalErrorHandling> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);

                await HandleResponseStatusCode(httpContext);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private static Task HandleResponseStatusCode(HttpContext httpContext)
        {
            var response = httpContext.Response;

            return httpContext.Response.StatusCode switch
            {
                StatusCodes.Status401Unauthorized => Handle401Unauthorized(response),
                StatusCodes.Status403Forbidden => Handle403Forbidden(response),
                StatusCodes.Status404NotFound => Handle404NotFound(response),
                _ => Task.CompletedTask
            };
        }

        private static async Task Handle401Unauthorized(HttpResponse response)
        {

            response.StatusCode = StatusCodes.Status401Unauthorized;
            response.ContentType = "application/json";

            var errorMessage = new
            {
                statusCode = StatusCodes.Status401Unauthorized,
                message = "Unauthorized. Your access token has expired.",
                reactivateEndpoint = "/api/login/reactivate/{userId}",
                instruction = "Send POST request to reactivate endpoint with your UserId to get a new access token."
            };

            await response.WriteAsJsonAsync(errorMessage);

        }
        private static async Task Handle403Forbidden(HttpResponse response)
        {
            if (response.HasStarted)
            {
                return;
            }

            response.Clear();
            response.StatusCode = StatusCodes.Status403Forbidden;
            response.ContentType = "application/json";

            var errorMessage = new
            {
                statusCode = StatusCodes.Status403Forbidden,
                message = "Access forbidden. You don't have permission to access this resource."
            };

            await response.WriteAsJsonAsync(errorMessage);
        }

        private static async Task Handle404NotFound(HttpResponse response)
        {
            if (response.HasStarted)
            {
                return;
            }

            response.Clear();
            response.StatusCode = StatusCodes.Status404NotFound;
            response.ContentType = "application/json";

            var errorMessage = new
            {
                statusCode = StatusCodes.Status404NotFound,
                message = "Resource not found."
            };

            await response.WriteAsJsonAsync(errorMessage);
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var response = httpContext.Response;

            if (response.HasStarted)
            {
                return;
            }

            response.Clear();
            response.StatusCode = StatusCodes.Status500InternalServerError;
            response.ContentType = "application/json";

            var errorMessage = new
            {
                statusCode = StatusCodes.Status500InternalServerError,
                message = "Internal server error. An unexpected error occurred.",
                details = exception?.Message
            };

            await response.WriteAsJsonAsync(errorMessage);
        }
    }
    public static class GlobalErrorHandlingExtension
    {
        public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandling>();
        }
    }
}
