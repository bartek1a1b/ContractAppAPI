using System.ComponentModel.DataAnnotations;

namespace ContractAppAPI.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}