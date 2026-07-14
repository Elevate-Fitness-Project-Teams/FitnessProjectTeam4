namespace FitnessCalculationEngine.Features.Metrics.DTOs;

public record FitnessMetricsDto(
    decimal Bmr,
    decimal Tdee,
    decimal CalorieTarget,
    string Status,
    DateTime CalculatedAt
);
