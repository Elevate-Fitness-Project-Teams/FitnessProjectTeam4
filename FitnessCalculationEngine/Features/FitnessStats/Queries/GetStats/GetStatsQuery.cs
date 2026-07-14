using FitnessCalculationEngine.Features.FitnessStats.DTOs;
using MediatR;

namespace FitnessCalculationEngine.Features.FitnessStats.Queries.GetStats;

public record GetStatsQuery(Guid UserId) : IRequest<FitnessStatsDto>;
