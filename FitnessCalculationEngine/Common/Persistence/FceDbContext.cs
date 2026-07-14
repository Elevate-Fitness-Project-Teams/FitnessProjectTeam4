using FitnessCalculationEngine.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FitnessCalculationEngine.Common.Persistence;

public class FceDbContext : DbContext
{
    public FceDbContext(DbContextOptions<FceDbContext> options) : base(options) { }

    public DbSet<UserFitnessStats> UserFitnessStats => Set<UserFitnessStats>();
    public DbSet<CalculatedMetrics> CalculatedMetrics => Set<CalculatedMetrics>();
    public DbSet<FitnessPlanConfig> FitnessPlanConfigs => Set<FitnessPlanConfig>();
    public DbSet<UserAssignedPlans> UserAssignedPlans => Set<UserAssignedPlans>();
    public DbSet<UserPlanHistory> UserPlanHistory => Set<UserPlanHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Use sequential (COMB) GUIDs by default for Guid PKs — they index better
        // on SQL Server's clustered indexes than Guid.NewGuid()'s random ones.
        // Never use Guid.NewGuid() in handlers; let EF call NewId.NextGuid() here.
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var idProp = entity.FindProperty("Id");
            if (idProp != null && idProp.ClrType == typeof(Guid))
            {
                idProp.SetValueGeneratorFactory((_, _) => new SequentialGuidValueGenerator());
            }
        }

        modelBuilder.Entity<UserFitnessStats>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.UserId, x.RecordedAt });
            b.Property(x => x.Gender).HasConversion<string>().HasMaxLength(20);
            b.Property(x => x.Goal).HasConversion<string>().HasMaxLength(30);
            b.Property(x => x.ActivityLevel).HasConversion<string>().HasMaxLength(20);
        });

        modelBuilder.Entity<CalculatedMetrics>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.UserId).IsUnique();
            b.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        });

        // FitnessPlanConfig — string PK (maps to Workout service plan keys).
        modelBuilder.Entity<FitnessPlanConfig>(b =>
        {
            b.HasKey(x => x.PlanId);
            b.HasIndex(x => new { x.Goal, x.Status }).IsUnique();
            b.Property(x => x.PlanId).HasMaxLength(50);
            b.Property(x => x.PlanName).HasMaxLength(100);
            b.Property(x => x.Description).HasMaxLength(500);
            b.Property(x => x.EstimatedDuration).HasMaxLength(50);
            b.Property(x => x.ProgramType).HasMaxLength(50);
            b.Property(x => x.Goal).HasConversion<string>().HasMaxLength(30);
            b.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        });

        modelBuilder.Entity<UserAssignedPlans>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.UserId, x.IsActive });
            b.HasIndex(x => x.UserId)
                .HasFilter("[IsActive] = 1")
                .IsUnique();
            b.Property(x => x.PlanId).HasMaxLength(50);
            b.HasOne(x => x.Plan)
                .WithMany()
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserPlanHistory>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.UserId);
            b.Property(x => x.PlanId).HasMaxLength(50);
            b.Property(x => x.ReasonForChange).HasMaxLength(255);
        });

        base.OnModelCreating(modelBuilder);
    }
}

internal sealed class SequentialGuidValueGenerator : Microsoft.EntityFrameworkCore.ValueGeneration.ValueGenerator<Guid>
{
    public override bool GeneratesTemporaryValues => false;
    public override Guid Next(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry) => NewId.NextGuid();
}
