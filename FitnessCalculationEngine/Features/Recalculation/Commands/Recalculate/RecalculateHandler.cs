using FitnessCalculationEngine.Common;
using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Common.Persistence;
using FitnessCalculationEngine.Domain.Entities;
using FitnessCalculationEngine.Features.Recalculation.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessCalculationEngine.Features.Recalculation.Commands.Recalculate;

public class RecalculateHandler(IRepository repository) : IRequestHandler<RecalculateCommand, RecalculateResultDto>
{
    public async Task<RecalculateResultDto> Handle(RecalculateCommand cmd, CancellationToken ct)
    {
        await using var tx = await repository.BeginTransactionAsync(ct);

        var metrics = await repository.Query<CalculatedMetrics>()
            .FirstOrDefaultAsync(m => m.UserId == cmd.UserId, ct)
            ?? throw new AppException(400, ErrorCodes.FCE_METRICS_NOT_CALCULATED, "Metrics have not been calculated yet. Call /calculate first.");

        var stats = await repository.Query<UserFitnessStats>()
            .Where(s => s.UserId == cmd.UserId)
            .OrderByDescending(s => s.RecordedAt)
            .FirstOrDefaultAsync(ct)
            ?? throw new AppException(400, ErrorCodes.FCE_STATS_NOT_FOUND, "No fitness stats found for this user.");

        var previousStatus = metrics.Status;
        var now = DateTime.UtcNow;

        if (cmd.NewWeight is not null)
        {
            var newStatsRow = new UserFitnessStats
            {
                UserId        = cmd.UserId,
                Weight        = cmd.NewWeight.Value,
                Height        = stats.Height,
                Age           = stats.Age,
                Gender        = stats.Gender,
                Goal          = stats.Goal,
                ActivityLevel = stats.ActivityLevel,
                RecordedAt    = now
            };
            repository.Add(newStatsRow);
            stats = newStatsRow;
        }

        var (bmr, tdee, calorieTarget, newStatus) = FitnessCalculator.Calculate(
            stats.Gender, stats.Weight, stats.Height, stats.Age, stats.ActivityLevel, stats.Goal);

        metrics.Bmr           = bmr;
        metrics.Tdee          = tdee;
        metrics.CalorieTarget = calorieTarget;
        metrics.Status        = newStatus;
        metrics.CalculatedAt  = now;

        var planReassignment = false;
        string? message = null;

        if (previousStatus != newStatus)
        {
            var newPlan = await repository.QueryNoTracking<FitnessPlanConfig>()
                .FirstOrDefaultAsync(p => p.Goal == stats.Goal && p.Status == newStatus, ct);

            if (newPlan is not null)
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
                        ReasonForChange = cmd.Reason ?? $"Recalculated ({cmd.TriggeredBy ?? "manual"})"
                    });
                }

                repository.Add(new UserAssignedPlans
                {
                    UserId     = cmd.UserId,
                    PlanId     = newPlan.PlanId,
                    AssignedAt = now,
                    IsActive   = true
                });

                planReassignment = true;
            }
            else
            {
                message = "Fitness status changed but no matching plan was found. Metrics were updated only.";
            }
        }

        try
        {
            await repository.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
        }
        catch (DbUpdateException)
        {
            throw new AppException(409, ErrorCodes.FCE_PLAN_ALREADY_ASSIGNED, "Another request already changed this user's active plan. Please retry.");
        }

        return new RecalculateResultDto(bmr, tdee, calorieTarget, newStatus.ToString(), planReassignment, message);
    }
}
