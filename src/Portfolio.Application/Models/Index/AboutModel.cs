using Portfolio.Application.Tools.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Index
{
    public class AboutModel
    {
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string BoldText { get; set; }

        [Required]
        [MaxLength(350)]
        public string MainText { get; set; }

        [Required]
        [MaxLength(100)]
        public string SideText { get; set; }

        [Display(Name = "Cv Link")]
        public bool ShowCvLink { get; set; } = false;

        [RequiredIfCheckedAttribute("ShowCvLink", true)]
        [Display(Name = "Cv Link")]
        public string CvLink { get; set; }

        public bool ShowLinkedinUrl { get; set; } = false;

        [RequiredIfCheckedAttribute("ShowLinkedinUrl", true)]
        [Display(Name = "Linkedin Url")]
        public string LinkedinUrl { get; set; }
        public bool ShowGithubUrl { get; set; } = false;

        [RequiredIfCheckedAttribute("ShowGithubUrl", true)]
        [Display(Name = "Github Link")]
        public string GithubUrl { get; set; }

        public string CoverUrl { get; set; }
    }
}