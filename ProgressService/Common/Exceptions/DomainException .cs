using ProgressService.Common.Extensions;
using ProgressService.Common.Responses;

namespace ProgressService.Common.Exceptions
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
