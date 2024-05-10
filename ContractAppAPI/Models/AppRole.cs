using Microsoft.AspNetCore.Identity;

namespace ContractAppAPI.Models
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}