

namespace WorkoutService.Common.Responses
{
    public class FailedResponseViewModel : ResponseViewModel
    {
        public ErrorCode ErrorCode { get; set; }

        public FailedResponseViewModel( string message, List<string> errors, ErrorCode errorCode, DateTime timestamp) 
        {
            IsSuccess = false;
            Message = message;
            ErrorCode = errorCode;
            Errors = errors;
            Timestamp = timestamp;
        }
    }
}
