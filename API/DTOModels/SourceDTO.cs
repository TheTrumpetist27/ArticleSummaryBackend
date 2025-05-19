using System.ComponentModel.DataAnnotations;

namespace API.DTOModels
{
    public class SourceDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public int ArticleId { get; set; }
        //public ArticleDTO Article { get; set; } = null!;
    }
}
