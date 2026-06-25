using System.ComponentModel.DataAnnotations;

namespace ProfileService.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
        public Guid? CreatedById { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedById { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        
    }
}
