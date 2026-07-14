namespace ProgressService.Features.Progress.Dtos
{
    public record WeightHistoryDto(Guid Id, double Weight, DateTime Date, string? Notes);
}
