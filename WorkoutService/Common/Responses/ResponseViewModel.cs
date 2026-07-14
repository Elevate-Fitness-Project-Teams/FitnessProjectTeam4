
namespace WorkoutService.Common.Responses
{
    public abstract class ResponseViewModel
    {
        public bool IsSuccess { get; init; }

        public string Message { get; init; } = default!;

        public List<string>? Errors { get; init; }

        public DateTime Timestamp { get; init; }
    }
}
