using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Identity
{
    public class AuthenticationModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(120)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(120)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; } = false;
    }
}
