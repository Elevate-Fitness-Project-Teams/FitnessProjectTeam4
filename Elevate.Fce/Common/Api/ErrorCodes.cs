namespace Elevate.Fce.Common.Api;

// FCE-relevant error code constants. Reference: backend blueprint.
public static class ErrorCodes
{
    // Auth
    public const string AUTH_TOKEN_EXPIRED = "AUTH_TOKEN_EXPIRED";
    public const string AUTH_TOKEN_INVALID = "AUTH_TOKEN_INVALID";

    // Validation
    public const string VAL_REQUIRED_FIELD = "VAL_REQUIRED_FIELD";
    public const string VAL_INVALID_AGE = "VAL_INVALID_AGE";
    public const string VAL_INVALID_WEIGHT = "VAL_INVALID_WEIGHT";
    public const string VAL_INVALID_HEIGHT = "VAL_INVALID_HEIGHT";
    public const string VAL_INVALID_GENDER = "VAL_INVALID_GENDER";

    // Resource
    public const string RES_NOT_FOUND = "RES_NOT_FOUND";
    public const string RES_USER_NOT_FOUND = "RES_USER_NOT_FOUND";

    // FCE-specific
    public const string FCE_METRICS_NOT_CALCULATED = "FCE_METRICS_NOT_CALCULATED";
    public const string FCE_STATS_NOT_FOUND = "FCE_STATS_NOT_FOUND";
    public const string FCE_NO_MATCHING_PLAN = "FCE_NO_MATCHING_PLAN";

    // Access / rate limiting
    public const string PERM_PREMIUM_REQUIRED = "PERM_PREMIUM_REQUIRED";
    public const string RATE_LIMIT_EXCEEDED = "RATE_LIMIT_EXCEEDED";
}
