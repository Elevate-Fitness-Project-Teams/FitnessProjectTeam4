using ProfileService.Common.Enum;

namespace ProfileService.Common.GenericResult
{
    public record RequestResult<T>(T data, string? message, bool isSuccess, ErrorCode errorCode)
    {
        public static RequestResult<T> Success(T data, string? message = null) =>
             new RequestResult<T>(data, null, true, ErrorCode.None);
        public static RequestResult<T> Failure(string? message, ErrorCode errorCode = ErrorCode.None) =>
            new RequestResult<T>(default!, message, false, errorCode);
        public static RequestResult<T> Failure(ErrorCode errorCode) =>
            new RequestResult<T>(default!, string.Empty, false, errorCode);
    }
}
