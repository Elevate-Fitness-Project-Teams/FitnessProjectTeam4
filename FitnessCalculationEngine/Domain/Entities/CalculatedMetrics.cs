using FitnessCalculationEngine.Domain.Enums;

namespace FitnessCalculationEngine.Domain.Entities;

public class CalculatedMetrics
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Bmr { get; set; }
    public decimal Tdee { get; set; }
    public decimal CalorieTarget { get; set; }
    public FitnessStatus Status { get; set; }
    public DateTime CalculatedAt { get; set; }
}
