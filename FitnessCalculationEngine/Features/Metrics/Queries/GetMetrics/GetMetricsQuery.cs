using FitnessCalculationEngine.Features.Metrics.DTOs;
using MediatR;

namespace FitnessCalculationEngine.Features.Metrics.Queries.GetMetrics;

public record GetMetricsQuery(Guid UserId) : IRequest<FitnessMetricsDto>;
