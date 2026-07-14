using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Common.Persistence;
using FitnessCalculationEngine.Domain.Entities;
using FitnessCalculationEngine.Features.Metrics.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessCalculationEngine.Features.Metrics.Queries.GetMetrics;

public class GetMetricsHandler(IRepository repository) : IRequestHandler<GetMetricsQuery, FitnessMetricsDto>
{
    public async Task<FitnessMetricsDto> Handle(GetMetricsQuery query, CancellationToken ct)
    {
        var dto = await repository.QueryNoTracking<CalculatedMetrics>()
            .Where(m => m.UserId == query.UserId)
            .Select(m => new FitnessMetricsDto(m.Bmr, m.Tdee, m.CalorieTarget, m.Status.ToString(), m.CalculatedAt))
            .FirstOrDefaultAsync(ct)
            ?? throw new AppException(400, ErrorCodes.FCE_METRICS_NOT_CALCULATED, "No metrics found. Call /calculate first.");

        return dto;
    }
}
