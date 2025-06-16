using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Summary { get; set; } = string.Empty;

        //public int DomainId { get; set; }
        //public Domain Domain { get; set; } = null!;
        
        //public int CreatedById { get; set; }
        //public User CreatedBy { get; set; } = null!;

        //public int? UpdatedById { get; set; }
        //public User? UpdatedBy { get; set; }

        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public DateTime? UpdatedAt { get; set; }

        //public int SourceId { get; set; }
        public Source Source { get; set; } = null!;
        public List<Comment> Comments { get; set; } = new();

    }
}
