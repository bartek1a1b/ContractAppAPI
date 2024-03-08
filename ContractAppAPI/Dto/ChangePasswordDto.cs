using System.ComponentModel.DataAnnotations;

namespace ContractAppAPI.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; }
    }
}