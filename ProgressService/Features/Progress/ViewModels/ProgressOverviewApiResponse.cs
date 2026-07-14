namespace ProgressService.Features.Progress.ViewModels
{
    public record ProgressOverviewApiResponse(Guid LogId, string UserId, DateTime CompletedAt, int DurationInMinutes, string Difficulty);

}
