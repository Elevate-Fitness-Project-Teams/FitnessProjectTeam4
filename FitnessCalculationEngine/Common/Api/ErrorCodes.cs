namespace FitnessCalculationEngine.Common.Api;

public static class ErrorCodes
{
    public const string AUTH_TOKEN_EXPIRED = "AUTH_TOKEN_EXPIRED";
    public const string AUTH_TOKEN_INVALID = "AUTH_TOKEN_INVALID";

    public const string VAL_REQUIRED_FIELD = "VAL_REQUIRED_FIELD";
    public const string VAL_INVALID_AGE = "VAL_INVALID_AGE";
    public const string VAL_INVALID_WEIGHT = "VAL_INVALID_WEIGHT";
    public const string VAL_INVALID_HEIGHT = "VAL_INVALID_HEIGHT";
    public const string VAL_INVALID_GENDER = "VAL_INVALID_GENDER";
    public const string VAL_INVALID_GOAL = "VAL_INVALID_GOAL";
    public const string VAL_INVALID_ACTIVITY = "VAL_INVALID_ACTIVITY";
    public const string VAL_INVALID_STATUS = "VAL_INVALID_STATUS";
    public const string VAL_INVALID_PAGE = "VAL_INVALID_PAGE";
    public const string VAL_INVALID_PAGE_SIZE = "VAL_INVALID_PAGE_SIZE";

    public const string RES_NOT_FOUND = "RES_NOT_FOUND";
    public const string RES_USER_NOT_FOUND = "RES_USER_NOT_FOUND";

    public const string FCE_METRICS_NOT_CALCULATED = "FCE_METRICS_NOT_CALCULATED";
    public const string FCE_STATS_NOT_FOUND = "FCE_STATS_NOT_FOUND";
    public const string FCE_NO_MATCHING_PLAN = "FCE_NO_MATCHING_PLAN";
    public const string RES_PLAN_NOT_FOUND = "RES_PLAN_NOT_FOUND";
    public const string FCE_PLAN_ALREADY_ASSIGNED = "FCE_PLAN_ALREADY_ASSIGNED";
    public const string FCE_INVALID_CALCULATION = "FCE_INVALID_CALCULATION";

    public const string PERM_PREMIUM_REQUIRED = "PERM_PREMIUM_REQUIRED";
    public const string RATE_LIMIT_EXCEEDED = "RATE_LIMIT_EXCEEDED";
}
