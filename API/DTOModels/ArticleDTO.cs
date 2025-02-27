using System.ComponentModel.DataAnnotations;

namespace API.DTOModels
{
    public class ArticleDTO
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Summary { get; set; } = string.Empty;

        public int DomainId { get; set; }
        public DomainDTO Domain { get; set; } = null!;
        
        public int CreatedById { get; set; }
        public UserDTO CreatedBy { get; set; } = null!;

        public int? UpdatedById { get; set; }
        public UserDTO? UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public List<SourceDTO> Sources { get; set; } = new();
    }
}
