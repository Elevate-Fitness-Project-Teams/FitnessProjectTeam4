using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.WorkoutPlans.Commands
{
    public class CreateWorkoutPlanCommandHandler(
        IGenericRepository<WorkoutPlan> _repository,
        IUnitOfWork _unitOfWork,
        ILogger<CreateWorkoutPlanCommandHandler> logger)
        : IRequestHandler<CreateWorkoutPlanCommand, RequestResult<Guid>>
    {
        public async Task<RequestResult<Guid>> Handle(CreateWorkoutPlanCommand request, CancellationToken ct)
        {
            logger.LogInformation("Creating a new workout plan structural catalog entry for title: {Title}", request.Title);
            try
            {
                var planId = await _unitOfWork.ExecuteAsync(async () =>
                {
                    var workoutPlan = new WorkoutPlan(request.Title, request.Description, request.Goal, request.UserId, request.UserTier, request.ExternalPlanId, request.Difficulty, request.Status);
                    await _repository.AddAsync(workoutPlan, ct);
                    return workoutPlan.Id;
                }, ct);

                return RequestResult<Guid>.Success(planId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Transaction aborted during plan creation context for user {UserId}", request.UserId);
                return RequestResult<Guid>.Failure(ErrorCode.InternalServerError, "An internal ledger execution error occurred.");
            }
        }
    }
}
