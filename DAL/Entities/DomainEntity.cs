using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class DomainEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public CompanyEntity Company { get; set; } = null!;

        public List<UserDomainRoleEntity> UserDomainRoles { get; set; } = new();
        public List<ArticleEntity> Articles { get; set; } = new();

    }
}
