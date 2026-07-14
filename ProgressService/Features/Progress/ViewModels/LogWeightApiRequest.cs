
namespace ProgressService.Features.Progress.ViewModels
{
    public record LogWeightApiRequest(
        double Weight,
        DateTime Date,
        string? Notes
    );
}
