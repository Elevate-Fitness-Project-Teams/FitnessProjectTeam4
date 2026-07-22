namespace NutritionService.Common
{
    public record Result<T>(
        bool IsSuccess,
        T? Data,
        string? ErrorMessage,
        int StatusCode)
    {
        public static Result<T> Success(T data, int statusCode = 200)
            => new(true, data, null, statusCode);
        public static Result<T> Failure(string errorMessage, int statusCode = 400)
            => new(false, default, errorMessage, statusCode);
    }
}
