
namespace WorkoutService.Common.Responses
{
    public class SuccessResponseViewModel<T> : ResponseViewModel
    {
        public T Data { get; set; }
        public SuccessResponseViewModel(T data , string message, List<string> errors, int statusCode, DateTime timestamp)
        {
            IsSuccess = true;
            Message = message;
            Data = data;
            Errors = errors;
            StatusCode = statusCode;
            Timestamp = timestamp;
        }
        public SuccessResponseViewModel(T data)
        {
            IsSuccess = true;
            Data = data;
        }
    }
}
