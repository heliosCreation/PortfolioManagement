using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Index
{
    public class ServiceModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
    }
}
