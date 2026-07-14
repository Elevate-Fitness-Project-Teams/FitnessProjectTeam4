using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Common.Persistence;
using FitnessCalculationEngine.Domain.Entities;
using FitnessCalculationEngine.Domain.Enums;
using FitnessCalculationEngine.Features.Plans.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessCalculationEngine.Features.Plans.Commands.AssignPlan;

public class AssignPlanHandler(IRepository repository) : IRequestHandler<AssignPlanCommand, AssignedPlanDto>
{
    public async Task<AssignedPlanDto> Handle(AssignPlanCommand cmd, CancellationToken ct)
    {
        var metricsStatus = await repository.QueryNoTracking<CalculatedMetrics>()
            .Where(m => m.UserId == cmd.UserId)
            .Select(m => (FitnessStatus?)m.Status)
            .FirstOrDefaultAsync(ct)
            ?? throw new AppException(400, ErrorCodes.FCE_METRICS_NOT_CALCULATED, "Metrics have not been calculated yet. Call /calculate first.");

        var statsGoal = await repository.QueryNoTracking<UserFitnessStats>()
            .Where(s => s.UserId == cmd.UserId)
            .OrderByDescending(s => s.RecordedAt)
            .Select(s => (Goal?)s.Goal)
            .FirstOrDefaultAsync(ct)
            ?? throw new AppException(400, ErrorCodes.FCE_STATS_NOT_FOUND, "No fitness stats found for this user.");

        var plan = await repository.QueryNoTracking<FitnessPlanConfig>()
            .FirstOrDefaultAsync(p => p.Goal == statsGoal && p.Status == metricsStatus, ct)
            ?? throw new AppException(404, ErrorCodes.FCE_NO_MATCHING_PLAN, "No fitness plan matches this user's goal and fitness status.");

        var now = DateTime.UtcNow;

        await using var tx = await repository.BeginTransactionAsync(ct);
        try
        {
            var activePlan = await repository.Query<UserAssignedPlans>()
                .FirstOrDefaultAsync(a => a.UserId == cmd.UserId && a.IsActive, ct);

            if (activePlan is not null)
            {
                activePlan.IsActive = false;

                repository.Add(new UserPlanHistory
                {
                    UserId          = cmd.UserId,
                    PlanId          = activePlan.PlanId,
                    AssignedAt      = activePlan.AssignedAt,
                    EndedAt         = now,
                    ReasonForChange = "Reassigned via /assign-plan"
                });
            }

            repository.Add(new UserAssignedPlans
            {
                UserId     = cmd.UserId,
                PlanId     = plan.PlanId,
                AssignedAt = now,
                IsActive   = true
            });

            await repository.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
        }
        catch (DbUpdateException)
        {
            throw new AppException(409, ErrorCodes.FCE_PLAN_ALREADY_ASSIGNED, "Another request already changed this user's active plan. Please retry.");
        }

        return new AssignedPlanDto(
            plan.PlanId,
            plan.PlanName,
            plan.Description,
            plan.Goal.ToString(),
            plan.Status.ToString(),
            plan.MinCalorie,
            plan.MaxCalorie,
            plan.EstimatedDuration,
            plan.WorkoutsPerWeek,
            plan.ProgramType,
            now
        );
    }
}
