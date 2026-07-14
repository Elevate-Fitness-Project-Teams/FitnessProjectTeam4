namespace FitnessCalculationEngine.Features.FitnessStats.DTOs;

public record FitnessStatsDto(
    decimal Weight,
    decimal Height,
    int Age,
    string Gender,
    string Goal,
    string ActivityLevel
);
