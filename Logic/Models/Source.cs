using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Source
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;
    }
}
