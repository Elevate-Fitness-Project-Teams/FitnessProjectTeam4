namespace FitnessCalculationEngine.Features.Calculations.DTOs;

public record CalculatedMetricsDto(
    decimal Bmr,
    decimal Tdee,
    decimal CalorieTarget,
    string Status
);
