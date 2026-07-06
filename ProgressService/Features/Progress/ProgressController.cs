using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgressService.Common.Responses;
using ProgressService.Features.Progress.Commands.LogWorkoutProgress;
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

            var userId = "user_user_123";

            var command = _mapper.Map<LogWorkoutProgressOrchestrator>(request);

            command = command with { UserId = userId };

            var result = await _mediator.Send(command, ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.Message ?? "Failed to Workout Logged Data.", new List<string>(), result.ErrorCode, DateTime.UtcNow);

           var response = _mapper.Map<LogProgressApiResponse>(result.Data);
            return new SuccessResponseViewModel<LogProgressApiResponse>(response, "Workout completion logged successfully.", new List<string>(), 201, DateTime.UtcNow);
        }
    }
}