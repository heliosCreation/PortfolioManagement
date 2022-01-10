using Microsoft.AspNetCore.Http;
using Portfolio.Application.Tools.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.Models.Index
{
    public class HomeMainDisplayModel
    {
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Your logo")]
        public string Logo { get; set; }

        [Display(Name = "Greeting text")]
        [MaxLength(15)]
        public string p { get; set; }

        [Display(Name = "Who you are")]
        [Required]
        [MaxLength(30)]
        public string h1 { get; set; }

        [Display(Name = "What you do")]
        [Required]
        [MaxLength(50)]
        public string h4 { get; set; }

        [Display(Name = "Show yourself")]
        public string CoverUrl { get; set; }

        [Display(Name = "Cover image")]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile CoverFile { get; set; }

    }
}
