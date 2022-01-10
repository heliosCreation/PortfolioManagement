using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Index
{
    public class InspirationModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Quote { get; set; }

        [Required]
        [MaxLength(120)]
        public string Author { get; set; }
    }
}
