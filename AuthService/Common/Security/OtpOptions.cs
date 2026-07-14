namespace AuthService.Common.Security;

public class OtpOptions
{
    public int TtlMinutes { get; set; } = 10;
    public int ResendCooldownSeconds { get; set; } = 30;
}
