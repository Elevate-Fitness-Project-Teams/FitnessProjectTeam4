using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace AuthService.Common.Api;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext ctx, Exception ex, CancellationToken ct)
    {
        var (status, code, message, errors) = ex switch
        {
            AppException app => (app.StatusCode, app.ErrorCode, app.Message, (IReadOnlyList<string>?)null),
            ValidationException val => (400, ErrorCodes.VAL_REQUIRED_FIELD, "Validation failed.",
                (IReadOnlyList<string>?)val.Errors.Select(e => e.ErrorMessage).ToList()),
            _ => (500, "INTERNAL_ERROR", "An unexpected error occurred.", (IReadOnlyList<string>?)null)
        };

        if (status >= 500)
            logger.LogError(ex, "Unhandled exception");

        ctx.Response.StatusCode = status;
        await ctx.Response.WriteAsJsonAsync(
            ApiResponse<object>.Fail(code, errors, status) with { Message = message },
            ct);
        return true;
    }
}
