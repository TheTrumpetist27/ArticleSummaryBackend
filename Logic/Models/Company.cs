using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public int CEOId { get; set; }
        //public User CEO { get; set; } = null!;
        //public List<Domain>? Domains { get; set; } = new();
    }
}