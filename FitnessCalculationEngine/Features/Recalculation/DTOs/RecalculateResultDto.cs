namespace FitnessCalculationEngine.Features.Recalculation.DTOs;

public record RecalculateResultDto(
    decimal Bmr,
    decimal Tdee,
    decimal CalorieTarget,
    string Status,
    bool PlanReassignment,
    string? Message
);
