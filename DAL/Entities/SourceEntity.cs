using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class SourceEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public int ArticleId { get; set; }
        public ArticleEntity Article { get; set; } = null!;
    }
}
