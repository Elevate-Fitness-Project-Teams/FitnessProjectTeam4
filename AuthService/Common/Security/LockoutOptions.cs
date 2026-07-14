namespace AuthService.Common.Security;

public class LockoutOptions
{
    public int MaxFailures { get; set; } = 5;
    public int WindowMinutes { get; set; } = 15;
    public int LockMinutes { get; set; } = 15;
}
