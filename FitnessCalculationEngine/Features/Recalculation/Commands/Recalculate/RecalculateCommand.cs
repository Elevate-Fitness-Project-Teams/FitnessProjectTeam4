using FitnessCalculationEngine.Features.Recalculation.DTOs;
using MediatR;

namespace FitnessCalculationEngine.Features.Recalculation.Commands.Recalculate;

public record RecalculateCommand(
    Guid UserId,
    string? Reason = null,
    decimal? NewWeight = null,
    string? TriggeredBy = null
) : IRequest<RecalculateResultDto>;
