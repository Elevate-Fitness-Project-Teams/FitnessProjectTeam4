using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutService.Common.Responses;
using WorkoutService.Features.Progress.Commands.LogWorkoutProgress;
using WorkoutService.Features.Progress.Dtos;
using WorkoutService.Features.Progress.ViewModels;

namespace WorkoutService.Features.Progress
{
    [ApiController]
    [Route("api/v1/progress")]
    public class ProgressController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpPost("workouts")]
        public async Task<ResponseViewModel> LogWorkoutProgress([FromBody] LogProgressApiRequest request, CancellationToken ct)
        {

            var userId = "user_999"; // for test

            var command = new LogWorkoutProgressCommand(
                request.WorkoutId,
                request.SessionId,
                userId,
                request.CompletedAt,
                request.Duration,
                request.CaloriesBurned,
                request.Difficulty,
                request.Notes,
                request.Rating,
                request.ExercisesCompleted.Select(e => new ExerciseCompletedDto(
                    e.ExerciseId, e.SetsCompleted, e.RepsCompleted, e.WeightUsed, e.Complete
                )).ToList()
            );

            var result = await _mediator.Send(command, ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.Message ?? "Failed to Log Workout Progress.", new List<string>(), result.ErrorCode, DateTime.UtcNow);

            var viewmodel = _mapper.Map<LogProgressApiResponseViewModel>(result.Data);
            return new SuccessResponseViewModel<LogProgressApiResponseViewModel>(viewmodel, " Workout Progress Logged successfully.", new List<string>(), 200, DateTime.UtcNow);

        }
    }
}
