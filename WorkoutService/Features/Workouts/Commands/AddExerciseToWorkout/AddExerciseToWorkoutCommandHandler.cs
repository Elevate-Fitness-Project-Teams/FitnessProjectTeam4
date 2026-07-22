using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Features.Common.Queries.CheckExerciseExists;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Workouts.Commands.AddExerciseToWorkout
{
    public class AddExerciseToWorkoutCommandHandler(
    IGenericRepository<Workout> _workoutRepository,
    IMediator _mediator,
    IUnitOfWork _unitOfWork,
    ILogger<AddExerciseToWorkoutCommandHandler> _logger
) : IRequestHandler<AddExerciseToWorkoutCommand, RequestResult<Unit>>
    {
        public async Task<RequestResult<Unit>> Handle(AddExerciseToWorkoutCommand request, CancellationToken ct)
        {
           _logger.LogInformation("Handling AddExerciseToWorkoutCommand for WorkoutId: {WorkoutId}, ExerciseId: {ExerciseId}", request.WorkoutId, request.ExerciseId);


            _logger.LogInformation("Fetching workout with ID: {WorkoutId}", request.WorkoutId);
            var workout = await _workoutRepository.GetAll().AsTracking()
                .Include(w => w.WorkoutExercises)
                .FirstOrDefaultAsync(w => w.Id == request.WorkoutId, ct);

            if (workout == null)
            {
                _logger.LogWarning("Workout with ID: {WorkoutId} not found.", request.WorkoutId);
                return RequestResult<Unit>.Failure(ErrorCode.WorkoutNotFound, "Workout not found.");
            }

            _logger.LogInformation("Checking if exercise with ID: {ExerciseId} exists.", request.ExerciseId);
            var exerciseExists = await _mediator.Send(new ExerciseExistQuery(request.ExerciseId), ct);
            if (!exerciseExists)
            {
                _logger.LogWarning("Exercise with ID: {ExerciseId} does not exist in the catalog.", request.ExerciseId);
                return RequestResult<Unit>.Failure(ErrorCode.ValidationError, "The selected exercise does not exist in the catalog.");
            }
            try
            {
                return await _unitOfWork.ExecuteAsync(async () =>
                {
                    _logger.LogInformation("Adding exercise with ID: {ExerciseId} to workout with ID: {WorkoutId}", request.ExerciseId, request.WorkoutId);

                    workout.AddExercise(request.ExerciseId, request.Sets, request.Reps, request.RestTimeInSeconds);

                    _workoutRepository.Update(workout);

                    _logger.LogInformation("Successfully added exercise with ID: {ExerciseId} to workout with ID: {WorkoutId}", request.ExerciseId, request.WorkoutId);
                    return RequestResult<Unit>.Success(Unit.Value);
                }, ct);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Validation error occurred while adding exercise with ID: {ExerciseId} to workout with ID: {WorkoutId}. Error: {ErrorMessage}", request.ExerciseId, request.WorkoutId, ex.Message);
                return RequestResult<Unit>.Failure(ErrorCode.ValidationError, ex.Message);
            }
        }
    }
}
