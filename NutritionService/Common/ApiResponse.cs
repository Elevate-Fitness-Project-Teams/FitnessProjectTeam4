namespace NutritionService.Common
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public static ApiResponse Fail(string message, int statusCode)
        {
            return new ApiResponse
            {
                IsSuccess = false,
                StatusCode = statusCode,
                Message = message
            };
        }
    }
}
