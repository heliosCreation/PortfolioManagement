using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Index
{
    public class SkillModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [Range(0,100)]
        public int Level { get; set; }

        public int Priority { get; set; } //TODO fluent validation greater than 0.
    }
}
