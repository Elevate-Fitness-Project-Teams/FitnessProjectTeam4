using System.Text.Json;

namespace NutritionService.Common.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                
                await _next(context);
            }
            catch (Exception ex)
            {
               
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);

                
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
           
            if (context.Response.HasStarted)
            {
                return;
            }

           
            context.Response.Clear();
            context.Response.ContentType = "application/json";

            
            var statusCode = StatusCodes.Status500InternalServerError;
            var errorCode = "INTERNAL_SERVER_ERROR";

            context.Response.StatusCode = statusCode;

            
            var response = Result<object>.Failure(errorCode, statusCode);

            
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
