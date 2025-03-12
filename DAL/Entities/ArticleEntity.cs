using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class ArticleEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Summary { get; set; } = string.Empty;

        public int DomainId { get; set; }
        public DomainEntity Domain { get; set; } = null!;
        
        public int CreatedById { get; set; }
        public UserEntity CreatedBy { get; set; } = null!;

        public int? UpdatedById { get; set; }
        public UserEntity? UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public List<SourceEntity> Sources { get; set; } = new();
    }
}
