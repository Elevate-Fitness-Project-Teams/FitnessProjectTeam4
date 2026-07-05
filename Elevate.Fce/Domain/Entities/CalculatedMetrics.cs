using Elevate.Fce.Domain.Enums;

namespace Elevate.Fce.Domain.Entities;

public class CalculatedMetrics
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public double Bmr { get; set; }
    public double Tdee { get; set; }
    public double CalorieTarget { get; set; }
    public FitnessStatus Status { get; set; }
    public DateTime CalculatedAt { get; set; }
}
