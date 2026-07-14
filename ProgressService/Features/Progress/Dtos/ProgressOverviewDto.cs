namespace ProgressService.Features.Progress.Dtos
{
    public record ProgressOverviewDto(Guid LogId, string UserId, DateTime CompletedAt, int DurationInMinutes, string Difficulty);
}
