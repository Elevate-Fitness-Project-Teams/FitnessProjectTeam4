using FitnessCalculationEngine.Domain.Enums;
using MediatR;

namespace FitnessCalculationEngine.Features.FitnessStats.Commands.SubmitStats;

public record SubmitStatsCommand(
    Guid UserId,
    decimal Weight,
    decimal Height,
    int Age,
    Gender Gender,
    Goal Goal,
    ActivityLevel ActivityLevel
) : IRequest<Guid>;
