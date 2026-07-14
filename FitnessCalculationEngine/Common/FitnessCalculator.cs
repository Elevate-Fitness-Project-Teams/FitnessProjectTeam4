using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Domain.Enums;

namespace FitnessCalculationEngine.Common;

public static class FitnessCalculator
{
    public static (decimal Bmr, decimal Tdee, decimal CalorieTarget, FitnessStatus Status) Calculate(
        Gender gender, decimal weight, decimal height, int age, ActivityLevel activityLevel, Goal goal)
    {
        var bmr           = CalculateBmr(gender, weight, height, age);
        var tdee          = bmr * ActivityFactor(activityLevel);
        var calorieTarget = tdee + GoalOffset(goal);
        var status        = ClassifyStatus(calorieTarget);
        return (bmr, tdee, calorieTarget, status);
    }

    private static decimal CalculateBmr(Gender gender, decimal weight, decimal height, int age) =>
        gender == Gender.Male
            ? 10m * weight + 6.25m * height - 5m * age + 5m
            : 10m * weight + 6.25m * height - 5m * age - 161m;

    private static decimal ActivityFactor(ActivityLevel level) => level switch
    {
        ActivityLevel.Rookie       => 1.200m,
        ActivityLevel.Beginner     => 1.375m,
        ActivityLevel.Intermediate => 1.550m,
        ActivityLevel.Advance      => 1.725m,
        ActivityLevel.TrueBeast    => 1.900m,
        _ => throw new AppException(400, ErrorCodes.FCE_INVALID_CALCULATION, "Unknown activity level.")
    };

    private static decimal GoalOffset(Goal goal) => goal switch
    {
        Goal.LoseWeight       => -500m,
        Goal.GainWeight       => +300m,
        Goal.GainMoreFlexible => +150m,
        Goal.GetFitter        => 0m,
        Goal.LearnTheBasic    => 0m,
        _ => throw new AppException(400, ErrorCodes.FCE_INVALID_CALCULATION, "Unknown goal.")
    };

    private static FitnessStatus ClassifyStatus(decimal calorieTarget) => calorieTarget switch
    {
        <= 1800m => FitnessStatus.Weak,
        <= 2500m => FitnessStatus.Normal,
        _        => FitnessStatus.Hard
    };
}
