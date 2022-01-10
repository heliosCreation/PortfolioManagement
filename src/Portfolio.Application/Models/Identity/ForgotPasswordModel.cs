using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Identity
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "You must provide your email adress"), EmailAddress]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
