using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutService.Common.Behaviors;
using WorkoutService.Common.Responses;
using WorkoutService.Features.Exercises.Queries.GetExerciseById;
using WorkoutService.Features.Exercises.Queries.GetPaginatedExercises;
using WorkoutService.Features.Exercises.ViewModels;

namespace WorkoutService.Features.Exercises
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ExercisesController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseViewModel> GetPaginated([FromQuery] GetExercisesApiRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPaginatedExercisesQuery(request.BodyPart, request.Equipment, request.SearchText, request.Page, request.PageSize), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.Message ?? "Failed to fetch Data.", new List<string>(), result.ErrorCode, DateTime.UtcNow);

            var mappedItems = _mapper.Map<List<ExerciseCardViewModel>>(result.Data!.Items);

            var pagedViewModel = new PaginatedList<ExerciseCardViewModel>(mappedItems, result.Data.TotalCount, result.Data.PageNumber, request.PageSize);

            return new SuccessResponseViewModel<PaginatedList<ExerciseCardViewModel>>(pagedViewModel, "Exercises fetched successfully.", new List<string>(), 200, DateTime.UtcNow);

        }

        [HttpGet("{id}")]
        public async Task<ResponseViewModel> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetExerciseByIdQuery(id), ct);

            if (!result.IsSuccess)
                return new FailedResponseViewModel(result.Message ?? "Failed to fetch Data.", new List<string>(), result.ErrorCode, DateTime.UtcNow);

            var viewModel = _mapper.Map<ExerciseDetailsViewModel>(result.Data);

            return new SuccessResponseViewModel<ExerciseDetailsViewModel>(viewModel, "Exercise fetched successfully.", new List<string>(), 200, DateTime.UtcNow);
        }
    }
}
