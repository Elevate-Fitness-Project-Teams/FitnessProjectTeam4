namespace WorkoutService.Features.Exercises.ViewModels
{
    public record GetExercisesApiRequest(
        string? BodyPart,
        string? Equipment,
        string? SearchText,
        int Page = 1,
        int PageSize = 10);
}
