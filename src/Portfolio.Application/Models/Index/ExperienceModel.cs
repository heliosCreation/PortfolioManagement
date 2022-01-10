using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Index
{
    public class ExperienceModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Position")]
        public string PositionName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; } = DateTime.Now;

        [MaxLength(450)]
        public string Details { get; set; } = "";
    }
}
