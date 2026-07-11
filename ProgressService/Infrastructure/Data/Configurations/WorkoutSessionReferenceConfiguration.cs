using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgressService.Domian.References;

namespace ProgressService.Infrastructure.Data.Configurations
{
    public class WorkoutSessionReferenceConfiguration : IEntityTypeConfiguration<WorkoutSessionReference>
    {
        public void Configure(EntityTypeBuilder<WorkoutSessionReference> builder)
        {
            builder.HasKey(x => x.SessionId);

            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .HasMaxLength(30);
        }
    }
}
