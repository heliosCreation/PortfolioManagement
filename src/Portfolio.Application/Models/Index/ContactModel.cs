using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Index
{
    public class ContactModel
    {
        public Guid? Id { get; set; }

        [Display(Name = "Your professional e-mail address")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "The place where you can be found")]
        [Required]
        public string Location { get; set; }

        [Display(Name = "Your phone number")]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
