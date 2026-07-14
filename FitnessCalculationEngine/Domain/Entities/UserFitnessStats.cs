using FitnessCalculationEngine.Domain.Enums;

namespace FitnessCalculationEngine.Domain.Entities;

// UserId comes from the JWT, never from the request body.
public class UserFitnessStats
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public Goal Goal { get; set; }
    public ActivityLevel ActivityLevel { get; set; }
    public DateTime RecordedAt { get; set; }
}
