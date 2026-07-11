namespace ProgressService.Features.Progress.ViewModels
{
    public record WeightHistoryApiResponse(Guid Id, double Weight, DateTime Date, string? Notes);
}
