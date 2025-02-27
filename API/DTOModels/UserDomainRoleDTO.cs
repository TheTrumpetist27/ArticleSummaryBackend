namespace API.DTOModels
{
    public class UserDomainRoleDTO
    {
        public int UserId { get; set; }
        public UserDTO User { get; set; } = null!;
        public int DomainId { get; set; }
        public DomainDTO Domain { get; set; } = null!;
        public Role Role { get; set; }
    }
}
