using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Index
{
    public class StudyModel
    {
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "School name")]
        [MaxLength(120)]
        public string SchoolName { get; set; }

        [Required]
        [MaxLength(120)]
        public string Diploma { get; set; }
    }
}
