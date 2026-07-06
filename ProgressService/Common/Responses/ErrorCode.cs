using System.ComponentModel;

namespace ProgressService.Common.Responses
{
    public enum ErrorCode
    {
        None = 0,

        // General 
        [Description("An unexpected error occurred.")]
        UnhandledException = 100,

        [Description("One or more validation errors occurred.")]
        ValidationError = 101,

        [Description("An internal server error occurred. Please try again later.")]
        InternalServerError = 102,

        [Description("The requested value cannot be null.")]
        NullValue = 103,

        [Description("You are not authorized to access this resource.")]
        UnAuthorized = 104,

        [Description("You do not have permission to perform this action.")]
        Forbidden = 105,

        [Description("A database error occurred.")]
        DatabaseError = 106,

        [Description("A concurrency conflict occurred. Please try again.")]
        ConcurrencyConflict = 107,


        // Workout Sessions
        [Description("The requested workout session was not found.")]
        WorkoutSessionNotFound = 200,

        [Description("The workout session has already started.")]
        WorkoutSessionAlreadyStarted = 201,

        [Description("The workout session has already been completed.")]
        WorkoutSessionAlreadyCompleted = 202,

        [Description("The workout session has been abandoned.")]
        WorkoutSessionAbandoned = 203,

        [Description("The workout session is not active.")]
        WorkoutSessionNotActive = 204,

        [Description("The workout session cannot be modified.")]
        WorkoutSessionModificationNotAllowed = 205,


        // Workout Progress 
        [Description("Workout progress was not found.")]
        ProgressNotFound = 300,

        [Description("Workout progress has already been logged.")]
        ProgressAlreadyLogged = 301,

        [Description("Workout progress cannot be logged for an incomplete session.")]
        ProgressCannotBeLogged = 302,

        [Description("Workout progress is invalid.")]
        InvalidWorkoutProgress = 303,

        [Description("Workout rating is invalid.")]
        InvalidWorkoutRating = 304,

        [Description("Workout duration is invalid.")]
        InvalidWorkoutDuration = 305,

        [Description("Calories burned value is invalid.")]
        InvalidCaloriesBurned = 306,


        // Exercise Progress
        [Description("The requested exercise progress was not found.")]
        ExerciseProgressNotFound = 400,

        [Description("Exercise has already been marked as completed.")]
        ExerciseAlreadyCompleted = 401,

        [Description("Exercise progress data is invalid.")]
        InvalidExerciseProgress = 402,

        [Description("Invalid number of completed sets.")]
        InvalidCompletedSets = 403,

        [Description("Invalid number of completed repetitions.")]
        InvalidCompletedReps = 404,

        [Description("Invalid exercise weight.")]
        InvalidExerciseWeight = 405,


        // Workout References 
        [Description("The referenced workout was not found.")]
        WorkoutReferenceNotFound = 500,

        [Description("The referenced exercise was not found.")]
        ExerciseReferenceNotFound = 501,

        [Description("The referenced user was not found.")]
        UserReferenceNotFound = 502,

        [Description("The workout reference is invalid.")]
        InvalidWorkoutReference = 503,


        // Weight History
        [Description("Weight history record was not found.")]
        WeightHistoryNotFound = 600,

        [Description("A weight entry for the specified date already exists.")]
        WeightEntryAlreadyExists = 601,

        [Description("The provided weight value is invalid.")]
        InvalidWeightValue = 602,

        [Description("Weight history cannot be deleted.")]
        WeightHistoryDeletionFailed = 603,


        // Body Measurements 
        [Description("Body measurement record was not found.")]
        BodyMeasurementNotFound = 700,

        [Description("A body measurement for the specified date already exists.")]
        BodyMeasurementAlreadyExists = 701,

        [Description("One or more body measurements are invalid.")]
        InvalidBodyMeasurement = 702,

        [Description("No body measurements were provided.")]
        BodyMeasurementEmpty = 703,


        // Achievements 
        [Description("The requested achievement was not found.")]
        AchievementNotFound = 800,

        [Description("Achievement has already been earned.")]
        AchievementAlreadyEarned = 801,

        [Description("Achievement requirements have not been met.")]
        AchievementRequirementsNotMet = 802,

        [Description("Achievement cannot be awarded.")]
        AchievementAwardFailed = 803,


        // User Achievements 
        [Description("User achievement record was not found.")]
        UserAchievementNotFound = 900,

        [Description("User has already unlocked this achievement.")]
        UserAlreadyHasAchievement = 901,


        // Streaks 
        [Description("User streak was not found.")]
        StreakNotFound = 1000,

        [Description("The streak has already been reset.")]
        StreakAlreadyReset = 1001,

        [Description("The streak cannot be updated.")]
        StreakUpdateFailed = 1002,

        [Description("Invalid streak value.")]
        InvalidStreakValue = 1003,


        // User Statistics 
        [Description("User statistics were not found.")]
        UserStatisticsNotFound = 1100,

        [Description("User statistics cannot be updated.")]
        UserStatisticsUpdateFailed = 1101,

        [Description("Current weight value is invalid.")]
        InvalidCurrentWeight = 1102,

        [Description("Start weight value is invalid.")]
        InvalidStartWeight = 1103,

        [Description("Total weight lost value is invalid.")]
        InvalidWeightLoss = 1104,
    }
}
