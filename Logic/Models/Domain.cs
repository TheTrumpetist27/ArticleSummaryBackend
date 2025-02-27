using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Domain
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public List<UserDomainRole> UserDomainRoles { get; set; } = new();
        public List<Article> Articles { get; set; } = new();

    }
}
