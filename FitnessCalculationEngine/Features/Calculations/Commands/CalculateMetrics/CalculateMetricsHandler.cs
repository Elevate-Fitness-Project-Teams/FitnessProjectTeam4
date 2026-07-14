using FitnessCalculationEngine.Common;
using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Common.Persistence;
using FitnessCalculationEngine.Domain.Entities;
using FitnessCalculationEngine.Features.Calculations.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessCalculationEngine.Features.Calculations.Commands.CalculateMetrics;

public class CalculateMetricsHandler(IRepository repository) : IRequestHandler<CalculateMetricsCommand, CalculatedMetricsDto>
{
    public async Task<CalculatedMetricsDto> Handle(CalculateMetricsCommand cmd, CancellationToken ct)
    {
        var stats = await repository.Query<UserFitnessStats>()
            .Where(s => s.UserId == cmd.UserId)
            .OrderByDescending(s => s.RecordedAt)
            .FirstOrDefaultAsync(ct)
            ?? throw new AppException(404, ErrorCodes.FCE_STATS_NOT_FOUND, "No fitness stats found for this user.");

        var (bmr, tdee, calorieTarget, status) = FitnessCalculator.Calculate(
            stats.Gender, stats.Weight, stats.Height, stats.Age, stats.ActivityLevel, stats.Goal);

        var existing = await repository.Query<CalculatedMetrics>()
            .FirstOrDefaultAsync(m => m.UserId == cmd.UserId, ct);

        try
        {
            if (existing is null)
            {
                repository.Add(new CalculatedMetrics
                {
                    UserId        = cmd.UserId,
                    Bmr           = bmr,
                    Tdee          = tdee,
                    CalorieTarget = calorieTarget,
                    Status        = status,
                    CalculatedAt  = DateTime.UtcNow
                });
            }
            else
            {
                existing.Bmr          = bmr;
                existing.Tdee         = tdee;
                existing.CalorieTarget = calorieTarget;
                existing.Status       = status;
                existing.CalculatedAt = DateTime.UtcNow;
            }

            await repository.SaveChangesAsync(ct);
        }
        catch (DbUpdateException) when (existing is null)
        {
            repository.ClearChangeTracker();

            existing = await repository.Query<CalculatedMetrics>()
                .FirstAsync(m => m.UserId == cmd.UserId, ct);

            existing.Bmr          = bmr;
            existing.Tdee         = tdee;
            existing.CalorieTarget = calorieTarget;
            existing.Status       = status;
            existing.CalculatedAt = DateTime.UtcNow;

            await repository.SaveChangesAsync(ct);
        }

        return new CalculatedMetricsDto(bmr, tdee, calorieTarget, status.ToString());
    }
}
