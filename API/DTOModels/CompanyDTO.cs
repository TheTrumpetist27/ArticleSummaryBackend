using System.ComponentModel.DataAnnotations;

namespace API.DTOModels
{
    public class CompanyDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public int CEOId { get; set; }
        public UserDTO CEO { get; set; } = null!;
        public List<DomainDTO> Domains { get; set; } = new();
    }
}
