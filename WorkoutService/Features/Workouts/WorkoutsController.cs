using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutService.Common.Behaviors;
using WorkoutService.Common.Responses;
using WorkoutService.Features.Workouts.Commands.CreateWorkout;
using WorkoutService.Features.Workouts.Commands.StartWorkout;
using WorkoutService.Features.Workouts.Queries.GetWorkoutById;
using WorkoutService.Features.Workouts.Queries.GetWorkouts;
using WorkoutService.Features.Workouts.Queries.GetWorkoutsByCategory;
using WorkoutService.Features.Workouts.Queries.GetWorkoutsByPlan;
using WorkoutService.Features.Workouts.ViewModels;

namespace WorkoutService.Features.Workouts
{
    [ApiController]
    [Route("api/V1/[controller]")]
    public class WorkoutsController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseViewModel> GetPaginated([FromQuery] GetWorkoutRequest request, CancellationToken ct)
        {
            
            var result = await _mediator.Send(new GetWorkoutsQuery(request.Category, request.Difficulty, request.SearchText, request.Page, request.PageSize), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel( result.Message ?? "Failed to fetch Data.", new List<string>(), result.ErrorCode, DateTime.UtcNow);

            var data = _mapper.Map<List<WorkoutViewModel>>(result.Data!.Items);

            var paginatedList = new PaginatedList<WorkoutViewModel>(data, result.Data.TotalCount, result.Data.PageNumber, request.PageSize);

            return new SuccessResponseViewModel<PaginatedList<WorkoutViewModel>>(paginatedList, "Workout fetched successfully.", new List<string>(), 200, DateTime.UtcNow);

        }

        [HttpGet("{id}")]
        public async Task<ResponseViewModel> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetWorkoutByIdQuery(id), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel( result.Message ?? "Failed to fetch Data.", new List<string>(), result.ErrorCode, DateTime.UtcNow);
            var viewModel = _mapper.Map<WorkoutDetailsViewModel>(result.Data);

                return new SuccessResponseViewModel<WorkoutDetailsViewModel>(viewModel, "Workout fetched successfully.", new List<string>(), 200, DateTime.UtcNow);
        }

        [HttpGet("by-plan/{planId}")]
        public async Task<ResponseViewModel> GetByPlanId([FromRoute] Guid planId, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetWorkoutsByPlanQuery(planId), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel( result.Message ?? "Failed to fetch Data.", new List<string>(), result.ErrorCode, DateTime.UtcNow);
            var viewModel = _mapper.Map<List<WorkoutViewModel>>(result.Data);

                return new SuccessResponseViewModel<List<WorkoutViewModel>>(viewModel, "Workout fetched successfully.", new List<string>(), 200, DateTime.UtcNow);
        }

        [HttpGet("category/{categoryName}")]
        public async Task<ResponseViewModel> GetCategory([FromRoute] string categoryName, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetWorkoutsByCategoryQuery(categoryName), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel( result.Message ?? "Failed to fetch Data.", new List<string>(), result.ErrorCode, DateTime.UtcNow);
            var viewModel = _mapper.Map<List<WorkoutViewModel>>(result.Data);

                return new SuccessResponseViewModel<List<WorkoutViewModel>>(viewModel, "Workout fetched successfully.", new List<string>(), 200, DateTime.UtcNow);
        }

        [HttpPost]
        public async Task<ResponseViewModel> Create([FromBody] CreateWorkoutApiRequest request, CancellationToken ct)
        {
     
            var result = await _mediator.Send(new CreateWorkoutCommand(
                request.WorkoutPlanId,
                request.Name, 
                request.Category, 
                request.Difficulty, 
                request.DurationInMinutes, 
                request.CaloriesBurn, 
                request.ImageUrl,
                request.IsPremium, 
                request.DayNumber), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.Message ?? "Failed to Create Workout.", new List<string>(), result.ErrorCode, DateTime.UtcNow);

            return new SuccessResponseViewModel<Guid>(result.Data, "Workout Created successfully.", new List<string>(), 201, DateTime.UtcNow);

        }

        [HttpPost("{id}/start")]
        public async Task<ResponseViewModel> StartWorkout([FromRoute] Guid id, CancellationToken ct)
        {
            var userId = Guid.NewGuid(); // Test

            var result = await _mediator.Send(new StartWorkoutCommand(id, userId), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.Message ?? "Failed to Start Session.", new List<string>(), result.ErrorCode, DateTime.UtcNow);

            return new SuccessResponseViewModel<Guid>(result.Data, "Workout session started successfully.", new List<string>(), 201, DateTime.UtcNow);
        }

    }
}
