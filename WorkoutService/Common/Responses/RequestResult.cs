namespace WorkoutService.Common.Responses
{
    public record RequestResult<TResult>
    {
        public TResult? Data { get; init; }
        public bool IsSuccess { get; init; }
        public ErrorCode ErrorCode { get; init; }
        public string? Message { get; init; } 

        private RequestResult(TResult? data, bool isSuccess, ErrorCode errorCode, string? message = null)
        {
            Data = data;
            IsSuccess = isSuccess;
            ErrorCode = errorCode;
            Message = message;
        }

        public static RequestResult<TResult> Success(TResult data) => new(data, true, ErrorCode.None);

        public static RequestResult<TResult> Failure(ErrorCode errorCode) => new(default, false, errorCode);

        public static RequestResult<TResult> Failure(ErrorCode errorCode, string message) => new(default, false, errorCode, message);
    }
}
