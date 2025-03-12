namespace DAL.Entities
{
    public class UserDomainRoleEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;
        public int DomainId { get; set; }
        public DomainEntity Domain { get; set; } = null!;
        public Role Role { get; set; }
    }
}
