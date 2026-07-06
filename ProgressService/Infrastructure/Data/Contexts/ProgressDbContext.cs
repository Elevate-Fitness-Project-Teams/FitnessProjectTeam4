using Microsoft.EntityFrameworkCore;
using ProgressService.Domian.Aggregates.UserStatistics;
using ProgressService.Domian.Aggregates.UserStreaks;
using ProgressService.Domian.Aggregates.WorkoutLogs;
using ProgressService.Domian.References;
using System.Diagnostics;

namespace ProgressService.Infrastructure.Data.Contexts
{
    public class ProgressDbContext(DbContextOptions<ProgressDbContext> options) : DbContext(options)
    {
        public DbSet<WorkoutLog> WorkoutLogs { get; set; }
        public DbSet<WorkoutLogExercise> WorkoutLogExercises { get; set; }
        public DbSet<UserStreak> UserStreaks { get; set; }
        public DbSet<UserStatistic> UserStatistics { get; set; }
        public DbSet<WorkoutSessionReference> WorkoutSessionReferences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.LogTo(log => Debug.WriteLine(log), LogLevel.Information).
                EnableSensitiveDataLogging(true); // Enable sensitive data logging for debugging purposes

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // Default tracking behavior set to NoTracking
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProgressDbContext).Assembly);
        }
    }
}
