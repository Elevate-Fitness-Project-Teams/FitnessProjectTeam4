using AuthService.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Common.Persistence;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<LoginAttempt> LoginAttempts => Set<LoginAttempt>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<OtpCode> OtpCodes => Set<OtpCode>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var idProp = entity.FindProperty("Id");
            if (idProp != null && idProp.ClrType == typeof(Guid))
                idProp.SetValueGeneratorFactory((_, _) => new SequentialGuidValueGenerator());
        }

        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Email).IsUnique();
            b.Property(x => x.Email).HasMaxLength(320).IsRequired();
            b.Property(x => x.PasswordHash).HasMaxLength(200).IsRequired();
            b.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            b.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            b.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();
        });

        modelBuilder.Entity<LoginAttempt>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.Email, x.AttemptedAt });
            b.Property(x => x.Email).HasMaxLength(320).IsRequired();
            b.Property(x => x.IpAddress).HasMaxLength(45);
        });

        modelBuilder.Entity<RefreshToken>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.TokenHash).IsUnique();
            b.HasIndex(x => new { x.UserId, x.RevokedAt });
            b.HasIndex(x => x.FamilyId);
            b.Property(x => x.TokenHash).HasMaxLength(128).IsRequired();
        });

        modelBuilder.Entity<OtpCode>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.Email, x.CreatedAt });
            b.Property(x => x.Email).HasMaxLength(320).IsRequired();
            b.Property(x => x.CodeHash).HasMaxLength(128).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}

internal sealed class SequentialGuidValueGenerator
    : Microsoft.EntityFrameworkCore.ValueGeneration.ValueGenerator<Guid>
{
    public override bool GeneratesTemporaryValues => false;
    public override Guid Next(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry) => NewId.NextGuid();
}
