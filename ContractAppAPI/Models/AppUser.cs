using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ContractAppAPI.Models
{
    public class AppUser : IdentityUser<int>
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
