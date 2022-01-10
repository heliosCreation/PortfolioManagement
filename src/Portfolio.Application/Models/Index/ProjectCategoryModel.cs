using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Index
{
    public class ProjectCategoryModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }
    }
}
