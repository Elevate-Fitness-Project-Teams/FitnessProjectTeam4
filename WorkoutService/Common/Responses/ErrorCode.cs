using System.ComponentModel;

namespace WorkoutService.Common.Responses
{
    public enum ErrorCode
    {
        None = 0,

        // --- General ---
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


        // --- Workouts ---
        [Description("The requested workout was not found.")]
        WorkoutNotFound = 200,

        [Description("A workout with this name already exists.")]
        WorkoutAlreadyExists = 201,

        [Description("The workout is inactive.")]
        WorkoutInactive = 202,

        [Description("The workout has already been deleted.")]
        WorkoutAlreadyDeleted = 203,

        [Description("The workout duration is invalid.")]
        InvalidWorkoutDuration = 204,

        [Description("The workout difficulty level is invalid.")]
        InvalidWorkoutDifficulty = 205,


        // --- Workout Plans ---
        [Description("The requested workout plan was not found.")]
        WorkoutPlanNotFound = 300,

        [Description("A workout plan with this name already exists.")]
        WorkoutPlanAlreadyExists = 301,

        [Description("The workout plan has already been deleted.")]
        WorkoutPlanAlreadyDeleted = 302,

        [Description("The workout plan contains no exercises.")]
        WorkoutPlanHasNoExercises = 303,

        [Description("The workout plan is already assigned.")]
        WorkoutPlanAlreadyAssigned = 304,


        // --- Exercises ---
        [Description("The requested exercise was not found.")]
        ExerciseNotFound = 400,

        [Description("An exercise with this name already exists.")]
        ExerciseAlreadyExists = 401,

        [Description("The exercise is already assigned to the workout.")]
        ExerciseAlreadyAssigned = 402,

        [Description("The exercise cannot be removed because it is in use.")]
        ExerciseInUse = 403,

        [Description("Invalid exercise configuration.")]
        InvalidExerciseConfiguration = 404,


        // --- Categories / Muscle Groups ---
        [Description("The requested category was not found.")]
        CategoryNotFound = 500,

        [Description("A category with this name already exists.")]
        CategoryAlreadyExists = 501,

        [Description("The category is currently in use.")]
        CategoryInUse = 502,

        [Description("The requested muscle group was not found.")]
        MuscleGroupNotFound = 503,


        // --- User Progress ---
        [Description("Workout progress was not found.")]
        ProgressNotFound = 600,

        [Description("Workout has already been completed.")]
        WorkoutAlreadyCompleted = 601,

        [Description("Workout session has not started.")]
        WorkoutNotStarted = 602,

        [Description("Workout session is already active.")]
        WorkoutAlreadyStarted = 603,

        [Description("Workout session not found.")]
        WorkoutSessionNotFound = 604,

    }
}
