using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutService.Common.Behaviors;
using WorkoutService.Common.Responses;
using WorkoutService.Features.WorkoutPlans.Commands;
using WorkoutService.Features.WorkoutPlans.Queries.GetWorkoutPlanById;
using WorkoutService.Features.WorkoutPlans.Queries.GetWorkoutPlans;
using WorkoutService.Features.WorkoutPlans.ViewModels;

namespace WorkoutService.Features.WorkoutPlans
{
    [ApiController]
    [Route("api/v1/workouts/plans")]
    public class WorkoutPlansController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {

        [HttpPost]
        public async Task<ResponseViewModel> CreatePlan([FromBody] CreateWorkoutPlanRequest request, CancellationToken ct = default)
        {
            var command = new CreateWorkoutPlanCommand(request.Title, request.Description, request.Status, request.UserTier, request.Goal, request.ExternalPlanId, request.UserId, request.UserTier);
            var result = await _mediator.Send(command, ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel( result.Message ?? "Failed to create workout plan.", new List<string>(), result.ErrorCode, DateTime.UtcNow);

            return new SuccessResponseViewModel<Guid>(result.Data!, "Workout plan created successfully.", new List<string>(), 201, DateTime.UtcNow);
        }

        [HttpGet]
        public async Task<ResponseViewModel> GetPlans(
            [FromQuery] string? userTier,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var result = await _mediator.Send(new GetWorkoutPlansQuery(userTier, pageNumber, pageSize), ct);

            if (!result.IsSuccess)
                 return new FailedResponseViewModel(result.Message ?? "Failed to fetch workout plans.", new List<string>(), result.ErrorCode, DateTime.UtcNow);
            var data = _mapper.Map<List<WorkoutPlanViewModel>>(result.Data!.Items);

            var paginatedList = new PaginatedList<WorkoutPlanViewModel>
                (
                   data,
                   result.Data.TotalCount,
                   result.Data.PageNumber,
                   pageSize
                );

            return new SuccessResponseViewModel<PaginatedList<WorkoutPlanViewModel>>(paginatedList, "Workout plans fetched successfully.", new List<string>(), 200, DateTime.UtcNow);
        }

        [HttpGet("{planId}")]
        public async Task<ResponseViewModel> GetById([FromRoute] Guid planId, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetWorkoutPlanByIdQuery(planId), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.Message ?? "Failed to fetch workout plans.", new List<string>(), result.ErrorCode, DateTime.UtcNow);

            var data = _mapper.Map<WorkoutPlanViewModel>(result.Data);
            return new SuccessResponseViewModel<WorkoutPlanViewModel>(data, "Workout plan fetched successfully.", new List<string>(), 200, DateTime.UtcNow);
        }
    }
}
