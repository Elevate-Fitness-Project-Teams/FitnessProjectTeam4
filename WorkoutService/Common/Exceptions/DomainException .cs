using WorkoutService.Common.Extensions;
using WorkoutService.Common.Responses;

namespace WorkoutService.Common.Exceptions
{
    public sealed class DomainException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public DomainException(ErrorCode errorCode) : base(errorCode.GetDescription())
        {
            ErrorCode = errorCode;
        }
    }
}
