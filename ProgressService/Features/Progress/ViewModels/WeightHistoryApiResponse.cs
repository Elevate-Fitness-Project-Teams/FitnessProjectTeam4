namespace ProgressService.Features.Progress.ViewModels
{
    public class WeightHistoryApiResponse
    {
        public Guid Id { get; init; }

        public double Weight { get; init; }

        public DateTime Date { get; init; }

        public string? Notes { get; init; }
    }
}
