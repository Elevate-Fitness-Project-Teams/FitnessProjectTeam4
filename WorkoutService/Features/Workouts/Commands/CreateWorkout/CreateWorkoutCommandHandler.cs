using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Workouts.Commands.CreateWorkout
{
    public class CreateWorkoutCommandHandler(
        IGenericRepository<WorkoutPlan> _repository,
        IUnitOfWork _unitOfWork,
        ILogger<CreateWorkoutCommandHandler> _logger
        ) : IRequestHandler<CreateWorkoutCommand, RequestResult<Guid>>
    {
        public async Task<RequestResult<Guid>> Handle(CreateWorkoutCommand request, CancellationToken ct)
        {
            _logger.LogInformation("Creating a new workout structural catalog entry for name: {Name} under plan: {PlanId}", request.Name, request.WorkoutPlanId);

            try
            {
                return await _unitOfWork.ExecuteAsync(async () =>
                {
                    _logger.LogDebug("Retrieving workout plan {PlanId} with workouts", request.WorkoutPlanId);

                    var workoutPlan = await _repository.GetAll().AsTracking()
                        .Include(p => p.Workouts)
                        .FirstOrDefaultAsync(p => p.Id == request.WorkoutPlanId, ct);

                    if (workoutPlan == null)
                    {
                        _logger.LogWarning("Workout plan with ID {PlanId} not found", request.WorkoutPlanId);
                        return RequestResult<Guid>.Failure(ErrorCode.WorkoutPlanNotFound, $"Workout plan with ID {request.WorkoutPlanId} not found.");
                    }

                    var workout = workoutPlan.AddWorkout(
                     request.Name,
                     request.DurationInMinutes,
                     request.Difficulty,
                     request.Category,
                     request.CaloriesBurn,
                     request.ImageUrl,
                     request.IsPremium,
                     request.DayNumber);

                    _logger.LogInformation("Workout {WorkoutId} added to workout plan {PlanId}", workout.Id, request.WorkoutPlanId);
                    return RequestResult<Guid>.Success(workout.Id);

                }, ct);

            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Validation failure during workout creation for plan {PlanId}: {Message}", request.WorkoutPlanId, ex.Message);
                return RequestResult<Guid>.Failure(ErrorCode.WorkoutPlanNotFound, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Domain rule violation during workout creation for plan {PlanId}: {Message}", request.WorkoutPlanId, ex.Message);
                return RequestResult<Guid>.Failure(ErrorCode.ValidationError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Transaction aborted during workout creation context for plan {PlanId}", request.WorkoutPlanId);
                return RequestResult<Guid>.Failure(ErrorCode.InternalServerError, "An internal ledger execution error occurred.");
            }
        }
    }
}
