using Microsoft.AspNetCore.Http;
using Portfolio.Application.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Portfolio.Application.Models.Index
{
    public class CreateProjectModel
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(500)]
        [Required]
        public string Description { get; set; }

        [MaxLength(250)]
        [Display(Name = "Github link")]
        public string GithubLink { get; set; }

        public List<string> Tools
        {
            get
            {
                return string.IsNullOrEmpty(ToolsString) ?
                        new List<string>()
                        : ToolsString
                        .Split(",").ToList()
                        .Where(c => !string.IsNullOrEmpty(c)).ToList();
            }
            set { }
        }
        public string ToolsString { get; set; }
        public List<ProjectCategoryModel> Categories { get; set; }
        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        public ProjectCategoryModel CategoryModel { get; set; }

        [Display(Name = "Cover image")]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile CoverFile { get; set; }
        public string CoverUrl { get; set; }

        [Display(Name = "Gallery images")]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryItemModel> GalleryItems { get; set; } = new List<GalleryItemModel>();

    }




}
