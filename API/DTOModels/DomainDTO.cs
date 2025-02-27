using System.ComponentModel.DataAnnotations;

namespace API.DTOModels
{
    public class DomainDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public CompanyDTO Company { get; set; } = null!;

        public List<UserDomainRoleDTO> UserDomainRoles { get; set; } = new();
        public List<ArticleDTO> Articles { get; set; } = new();

    }
}
