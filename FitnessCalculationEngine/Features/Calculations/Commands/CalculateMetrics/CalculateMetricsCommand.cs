using FitnessCalculationEngine.Features.Calculations.DTOs;
using MediatR;

namespace FitnessCalculationEngine.Features.Calculations.Commands.CalculateMetrics;

public record CalculateMetricsCommand(Guid UserId) : IRequest<CalculatedMetricsDto>;
