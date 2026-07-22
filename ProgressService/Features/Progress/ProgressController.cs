using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgressService.Common.Responses;
using ProgressService.Features.Progress.Commands.LogWeight;
using ProgressService.Features.Progress.Commands.LogWorkoutProgress;
using ProgressService.Features.Progress.Queries.GetAllProgress;
using ProgressService.Features.Progress.Queries.GetUserProfileProgress;
using ProgressService.Features.Progress.Queries.GetUserStats;
using ProgressService.Features.Progress.Queries.GetWeightHistory;
using ProgressService.Features.Progress.ViewModels;

namespace ProgressService.Features.Progress
{
    [ApiController]
    [Route("api/v1/progress")]
    public class ProgressController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {

        [HttpPost("workouts")]
        public async Task<ResponseViewModel> LogProgress([FromBody] LogProgressApiRequest request, CancellationToken ct)
        {

            Guid userId = Guid.Parse("56b58013-715f-4f4d-9fa7-6c5f0b0db996"); // Test

            var command = new LogWorkoutProgressOrchestrator(
                  request.WorkoutId,
                  request.SessionId,
                  userId,
                  request.CompletedAt,
                  request.Duration,
                  request.CaloriesBurned,
                  request.Difficulty,
                  request.Notes,
                  request.Rating,
                  request.ExercisesCompleted
                      .Select(x => new ExerciseCompletedDto(
                          x.ExerciseId,
                          x.SetsCompleted,
                          x.RepsCompleted,
                          x.WeightUsed,
                          x.Completed))
                      .ToList()
            );

            var result = await _mediator.Send(command, ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.ErrorCode, DateTime.UtcNow,"Failed to Workout Logged Data.", result.Errors?.ToList()) ;

           var response = _mapper.Map<LogProgressApiResponse>(result.Data);
            return new SuccessResponseViewModel<LogProgressApiResponse>(response, "Workout completion logged successfully.", DateTime.UtcNow);
        }

        [HttpPost("weight")]
        public async Task<ResponseViewModel> LogWeight([FromBody] LogWeightApiRequest request, CancellationToken ct)
        {
            var currentUserId = "25d02245-0cf1-4cb3-8cc2-60beec58a193";

            var result = await _mediator.Send(new LogWeightCommand(currentUserId, request.Weight, request.Date, request.Notes), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.ErrorCode, DateTime.UtcNow, result.Message, result.Errors?.ToList());

            var apiResponse = _mapper.Map<LogWeightApiResponse>(result.Data);
            return new SuccessResponseViewModel<LogWeightApiResponse>(apiResponse, "Weight logged successfully.", DateTime.UtcNow);
        }

        [HttpGet("weight-history/{userId}")]
        public async Task<ResponseViewModel> GetWeightHistory([FromRoute] string userId, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetWeightHistoryQuery(userId), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.ErrorCode, DateTime.UtcNow, result.Message, result.Errors?.ToList());

            var viewModel = _mapper.Map<List<WeightHistoryApiResponse>>(result.Data);
            return new SuccessResponseViewModel<List<WeightHistoryApiResponse>>(viewModel);
        }

        [HttpGet("stats/{userId}")]
        public async Task<ResponseViewModel> GetUserStats([FromRoute] string userId, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserStatsQuery(userId), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.ErrorCode, DateTime.UtcNow, result.Message, result.Errors?.ToList());

            var viewModel = _mapper.Map<UserStatsApiResponse>(result.Data);
            return new SuccessResponseViewModel<UserStatsApiResponse>(viewModel);
        }

        [HttpGet]
        public async Task<ResponseViewModel> GetAllProgress(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllProgressQuery(), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.ErrorCode, DateTime.UtcNow, result.Message, result.Errors?.ToList());

            var viewModel = _mapper.Map<List<ProgressOverviewApiResponse>>(result.Data);
            return new SuccessResponseViewModel<List<ProgressOverviewApiResponse>>(viewModel);
        }

        [HttpGet("{userId}")]
        public async Task<ResponseViewModel> GetUserProfileProgress([FromRoute] string userId, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserProfileProgressQuery(userId), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.ErrorCode, DateTime.UtcNow, result.Message, result.Errors?.ToList());

            var viewModel = _mapper.Map<CombinedProgressProfileApiResponse>(result.Data);
            return new SuccessResponseViewModel<CombinedProgressProfileApiResponse>(viewModel);
        }
    }
}