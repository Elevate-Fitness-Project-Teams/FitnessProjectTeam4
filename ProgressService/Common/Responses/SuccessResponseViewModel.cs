namespace ProgressService.Common.Responses
{
    public class SuccessResponseViewModel<T> : ResponseViewModel
    {
        public T Data { get; init; }

        public SuccessResponseViewModel(T data, string message, DateTime timestamp)
        {
            IsSuccess = true;

            Data = data;

            Message = message;

            Timestamp = timestamp;
        }

        public SuccessResponseViewModel(T data)
        {
            IsSuccess = true;

            Data = data;

            Message = "Operation completed successfully.";

            Timestamp = DateTime.UtcNow;
        }
    }
}
