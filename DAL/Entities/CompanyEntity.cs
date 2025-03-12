using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class CompanyEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public int CEOId { get; set; }
        public UserEntity CEO { get; set; } = null!;
        public List<DomainEntity> Domains { get; set; } = new();
    }
}
