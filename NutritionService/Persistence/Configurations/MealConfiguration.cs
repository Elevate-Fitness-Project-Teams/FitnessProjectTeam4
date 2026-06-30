using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionService.Domain.Entities;

namespace NutritionService.Persistence.Configurations
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.ToTable("Meals");
            builder.HasKey(e => e.MealId);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Type).IsRequired().HasConversion<string>().HasMaxLength(20);
            builder.Property(e => e.Difficulty).IsRequired().HasConversion<string>().HasMaxLength(20);
            builder.Property(e => e.ImageUrl).HasMaxLength(500);
            builder.Property(e => e.IngredientsJson).IsRequired().HasColumnType("text");
            builder.Property(e => e.InstructionsJson).IsRequired().HasColumnType("text");
            builder.Property(e => e.AllergensJson).HasMaxLength(500);
            builder.Property(e => e.TagsJson).HasMaxLength(255);
            builder.Property(e => e.VariationsJson).HasMaxLength(500);
            builder.HasQueryFilter(e => !e.IsDeleted);


        }
    }
}
