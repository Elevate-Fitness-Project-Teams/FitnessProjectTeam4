namespace AuthService.Common.Api;

public record ApiResponse<T>(
    bool IsSuccess,
    string? Message,
    T? Data,
    IReadOnlyList<string>? Errors,
    int StatusCode,
    DateTime Timestamp)
{
    public static ApiResponse<T> Ok(T data, string? message = null, int statusCode = 200) =>
        new(true, message, data, null, statusCode, DateTime.UtcNow);

    public static ApiResponse<T> Fail(string message, IReadOnlyList<string>? errors = null, int statusCode = 400) =>
        new(false, message, default, errors, statusCode, DateTime.UtcNow);
}
