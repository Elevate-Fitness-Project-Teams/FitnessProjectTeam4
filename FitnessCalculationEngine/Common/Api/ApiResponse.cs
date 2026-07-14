namespace FitnessCalculationEngine.Common.Api;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public IReadOnlyList<string>? Errors { get; set; }
    public int StatusCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResponse<T> Ok(T data, string? message = null, int statusCode = 200) =>
        new() { IsSuccess = true, Data = data, Message = message, StatusCode = statusCode };

    public static ApiResponse<T> Fail(string message, IReadOnlyList<string>? errors = null, int statusCode = 400) =>
        new() { IsSuccess = false, Message = message, Errors = errors, StatusCode = statusCode };
}
