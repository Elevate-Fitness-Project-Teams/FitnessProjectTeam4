namespace ProgressService.Features.Progress.Dtos
{
    public class WeightHistoryDto
    {
        public Guid Id { get; init; }

        public double Weight { get; init; }

        public DateTime Date { get; init; }

        public string? Notes { get; init; }
    }
}
