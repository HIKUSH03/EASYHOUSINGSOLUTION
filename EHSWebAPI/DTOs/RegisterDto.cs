using System.ComponentModel.DataAnnotations;

namespace EHSWebAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(25, ErrorMessage = "Username must not exceed 25 characters.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Password must not exceed 25 characters.")]
        public string Password { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "UserType must not exceed 15 characters.")]
        public string UserType { get; set; }
    }
}