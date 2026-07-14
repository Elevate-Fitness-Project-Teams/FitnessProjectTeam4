namespace AuthService.Common.Email;

public class ConsoleEmailSender(ILogger<ConsoleEmailSender> logger) : IEmailSender
{
    public Task SendAsync(string to, string subject, string body, CancellationToken ct)
    {
        logger.LogWarning("SMTP not configured — email suppressed. To: {To} | Subject: {Subject} | Body: {Body}",
            to, subject, body);
        return Task.CompletedTask;
    }
}
