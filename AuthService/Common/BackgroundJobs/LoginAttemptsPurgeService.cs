using AuthService.Common.Persistence;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Common.BackgroundJobs;

public class LoginAttemptsPurgeService(
    IServiceProvider services,
    ILogger<LoginAttemptsPurgeService> logger) : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromHours(24);
    private static readonly TimeSpan Retention = TimeSpan.FromDays(30);

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                using var scope = services.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IRepository>();
                var cutoff = DateTime.UtcNow - Retention;

                var deleted = await repo.Query<LoginAttempt>()
                    .Where(a => a.AttemptedAt < cutoff)
                    .ExecuteDeleteAsync(ct);

                if (deleted > 0)
                    logger.LogInformation("Purged {Count} LoginAttempts older than {Cutoff:O}.", deleted, cutoff);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                logger.LogError(ex, "LoginAttempts purge failed; will retry next cycle.");
            }

            try { await Task.Delay(Interval, ct); }
            catch (OperationCanceledException) { }
        }
    }
}
