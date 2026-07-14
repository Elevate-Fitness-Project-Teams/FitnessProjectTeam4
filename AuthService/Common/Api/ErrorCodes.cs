namespace AuthService.Common.Api;

public static class ErrorCodes
{
    public const string VAL_REQUIRED_FIELD = "VAL_REQUIRED_FIELD";
    public const string VAL_INVALID_FORMAT = "VAL_INVALID_FORMAT";
    public const string VAL_INVALID_LENGTH = "VAL_INVALID_LENGTH";

    public const string AUTH_EMAIL_EXISTS = "AUTH_EMAIL_EXISTS";
    public const string AUTH_WEAK_PASSWORD = "AUTH_WEAK_PASSWORD";
    public const string AUTH_INVALID_PHONE = "AUTH_INVALID_PHONE";

    public const string AUTH_INVALID_CREDENTIALS = "AUTH_INVALID_CREDENTIALS";
    public const string AUTH_ACCOUNT_LOCKED = "AUTH_ACCOUNT_LOCKED";

    public const string AUTH_TOKEN_EXPIRED = "AUTH_TOKEN_EXPIRED";
    public const string AUTH_TOKEN_INVALID = "AUTH_TOKEN_INVALID";

    public const string AUTH_INVALID_OTP = "AUTH_INVALID_OTP";
    public const string AUTH_OTP_EXPIRED = "AUTH_OTP_EXPIRED";
    public const string RATE_OTP_RESEND_TOO_SOON = "RATE_OTP_RESEND_TOO_SOON";

    public const string AUTH_PASSWORD_MISMATCH = "AUTH_PASSWORD_MISMATCH";
    public const string AUTH_RESET_TOKEN_INVALID = "AUTH_RESET_TOKEN_INVALID";

    public const string RATE_LIMIT_EXCEEDED = "RATE_LIMIT_EXCEEDED";
}
