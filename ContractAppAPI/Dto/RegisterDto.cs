using System.ComponentModel.DataAnnotations;

namespace ContractAppAPI.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}