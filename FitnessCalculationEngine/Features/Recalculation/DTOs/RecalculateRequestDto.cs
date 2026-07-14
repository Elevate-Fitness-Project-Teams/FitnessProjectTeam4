namespace FitnessCalculationEngine.Features.Recalculation.DTOs;

public record RecalculateRequestDto(
    string? Reason,
    decimal? NewWeight,
    string? TriggeredBy
);
