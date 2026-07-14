using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace FitnessCalculationEngine.Common.Api;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext ctx, Exception ex, CancellationToken ct)
    {
        (int status, string message, IReadOnlyList<string>? errors) = ex switch
        {
            AppException app     => (app.StatusCode, app.Message, (IReadOnlyList<string>?)[app.ErrorCode]),
            ValidationException val => (400, "Validation failed.", (IReadOnlyList<string>?)val.Errors.Select(e => e.ErrorCode).ToList()),
            _                    => (500, "An unexpected error occurred.", null)
        };

        if (status == 500)
            logger.LogError(ex, "Unhandled exception on {Method} {Path}", ctx.Request.Method, ctx.Request.Path);

        ctx.Response.StatusCode = status;
        await ctx.Response.WriteAsJsonAsync(ApiResponse<object>.Fail(message, errors, status), ct);
        return true;
    }
}
