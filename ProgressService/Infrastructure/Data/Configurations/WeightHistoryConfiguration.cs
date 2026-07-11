using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgressService.Domian.Aggregates.WeightHistories;

namespace ProgressService.Infrastructure.Data.Configurations
{
    public class WeightHistoryConfiguration : IEntityTypeConfiguration<WeightHistory>
    {
        public void Configure(EntityTypeBuilder<WeightHistory> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).ValueGeneratedNever();

            builder.Property(w => w.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(w => w.Weight)
                .IsRequired()
                .HasColumnType("decimal(5,2)"); 

            builder.Property(w => w.Date)
                .IsRequired();

            builder.Property(w => w.Notes)
                .HasMaxLength(500);
        }
    }
}
