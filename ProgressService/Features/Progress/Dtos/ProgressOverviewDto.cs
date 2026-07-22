namespace ProgressService.Features.Progress.Dtos
{
    public class ProgressOverviewDto
    {
        public Guid LogId { get; init; }

        public string UserId { get; init; } = null!;

        public DateTime CompletedAt { get; init; }

        public int DurationInMinutes { get; init; }

        public string Difficulty { get; init; } = null!;
    }
}
