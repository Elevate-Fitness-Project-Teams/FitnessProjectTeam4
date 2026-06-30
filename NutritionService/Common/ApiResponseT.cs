namespace NutritionService.Common
{
    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Data = data,
                StatusCode = statusCode,
                Message = null
            };
        }

        public new static ApiResponse<T> Fail(string message, int statusCode)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Data = default,
                StatusCode = statusCode,
                Message = message
            };
        }
    }
}
