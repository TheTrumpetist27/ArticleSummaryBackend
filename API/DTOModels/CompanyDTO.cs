using System.ComponentModel.DataAnnotations;

namespace API.DTOModels
{
    public class CompanyDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is verplicht.")]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "CEO is verplicht.")]
        public int CEOId { get; set; }
    }
}
