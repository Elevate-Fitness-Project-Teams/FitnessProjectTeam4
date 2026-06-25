using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProfileService.Common.Services.Interfaces;
using ProfileService.Domain.Common;
using ProfileService.Domain.Entities;
using System.Reflection;

namespace ProfileService.Infrastructure.Data
{
    public class ProfileDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ProfileDbContext(DbContextOptions<ProfileDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }
        public DbSet<PrivacySettings> PrivacySettings { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<UserStatisticCache> UserStatisticsCache { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());





            var mockUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var fixedDate = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<UserProfile>().HasData(
                new UserProfile
                {
                    Id = mockUserId,
                    FirstName = "Hazem",
                    LastName = "Mofdi",
                    Email = "test@developer.com",
                    ProfilePictureUrl = "https://example.com/avatar.png",
                    IsPremiumCached = false,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedOn = fixedDate
                }
            );

            modelBuilder.Entity<UserStatisticCache>().HasData(
                new UserStatisticCache
                {
                    Id = mockUserId,
                    TotalWorkouts = 25,
                    CurrentStreak = 5,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedOn = fixedDate
                }
            );
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.CreatedById = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                    entry.Entity.UpdatedById = userId;

                    entry.Property(x => x.CreatedOn).IsModified = false;
                    entry.Property(x => x.CreatedById).IsModified = false;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
