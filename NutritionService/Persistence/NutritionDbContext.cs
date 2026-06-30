using Microsoft.EntityFrameworkCore;
using NutritionService.Domain.Entities;
namespace NutritionService.Persistence
{
    public class NutritionDbContext : DbContext
    {
        public NutritionDbContext(DbContextOptions<NutritionDbContext> options) : base(options)
        {
        }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<NutritionFacts> NutritionFacts { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<MealPlanItem> MealPlanItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NutritionDbContext).Assembly);
        }
    }
}
