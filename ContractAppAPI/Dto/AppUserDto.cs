using ContractAppAPI.Models;

namespace ContractAppAPI.Dto
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public virtual RoleDto Role { get; set; }
    }
}