using ProfileService.Common.Enum;

namespace ProfileService.Common.GenericResult
{
    public record ResponseViewModel<T>(T data, bool isSuccess, string message, ErrorCode errorCode)
    {
        public static ResponseViewModel<T> Success(T data, string message = "Request was successful") =>
           new ResponseViewModel<T>(data, true, message, ErrorCode.None);
        public static ResponseViewModel<T> Failure(string message, ErrorCode errorCode = ErrorCode.None) =>
            new ResponseViewModel<T>(default!, false, message, errorCode);
        public static ResponseViewModel<T> Failure(ErrorCode errorCode) =>
            new ResponseViewModel<T>(default!, false, errorCode.ToString(), errorCode);
    }
}
