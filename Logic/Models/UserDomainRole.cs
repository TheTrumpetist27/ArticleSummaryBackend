namespace Core.Models
{
    public class UserDomainRole
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int DomainId { get; set; }
        public Domain Domain { get; set; } = null!;
    }
}
