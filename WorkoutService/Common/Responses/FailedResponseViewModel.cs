

using WorkoutService.Common.Extensions;

namespace WorkoutService.Common.Responses
{
    public class FailedResponseViewModel : ResponseViewModel
    {
        public ErrorCode ErrorCode { get; init; }

        public FailedResponseViewModel( ErrorCode errorCode, DateTime timestamp, string? message = null, List<string>? errors = null)
        {
            IsSuccess = false;

            ErrorCode = errorCode;

            Message = message ?? errorCode.GetDescription();

            Errors = errors;

            Timestamp = timestamp;
        }
    }
}
