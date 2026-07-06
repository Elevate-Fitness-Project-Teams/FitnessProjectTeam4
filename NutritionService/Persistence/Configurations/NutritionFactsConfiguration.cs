using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionService.Domain.Entities;

namespace NutritionService.Persistence.Configurations
{
    public class NutritionFactsConfiguration : IEntityTypeConfiguration<NutritionFacts>
    {
        public void Configure(EntityTypeBuilder<NutritionFacts> builder)
        {
            builder.ToTable("NutritionFacts");
            builder.HasKey(e => e.Id);
            builder.HasOne(nf => nf.Meal)
                .WithOne(m => m.NutritionFacts)
                .HasForeignKey<NutritionFacts>(nf => nf.MealId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
