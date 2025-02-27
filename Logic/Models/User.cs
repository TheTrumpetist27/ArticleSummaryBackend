using Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public Role Role { get; set; } = Role.Employee;
        public List<UserDomainRole> UserDomainRoles { get; set; } = new();
    }

    public enum Role
    {
        ApplicationOwner,
        CEO,
        Supervisor,
        Employee
    }
}
