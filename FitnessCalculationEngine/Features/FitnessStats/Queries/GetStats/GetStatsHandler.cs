using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Common.Persistence;
using FitnessCalculationEngine.Domain.Entities;
using FitnessCalculationEngine.Features.FitnessStats.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessCalculationEngine.Features.FitnessStats.Queries.GetStats;

public class GetStatsHandler(IRepository repository) : IRequestHandler<GetStatsQuery, FitnessStatsDto>
{
    public async Task<FitnessStatsDto> Handle(GetStatsQuery query, CancellationToken ct)
    {
        var dto = await repository.QueryNoTracking<UserFitnessStats>()
            .Where(s => s.UserId == query.UserId)
            .OrderByDescending(s => s.RecordedAt)
            .Select(s => new FitnessStatsDto(
                s.Weight,
                s.Height,
                s.Age,
                s.Gender.ToString(),
                s.Goal.ToString(),
                s.ActivityLevel.ToString()))
            .FirstOrDefaultAsync(ct)
            ?? throw new AppException(404, ErrorCodes.FCE_STATS_NOT_FOUND, "No fitness stats found for this user.");

        return dto;
    }
}
