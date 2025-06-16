using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class CommentEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; } = null!;
        //public string Author { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int ArticleId { get; set; }
        public ArticleEntity Article { get; set; } = null!;
    }
}
