using System;
using System.Collections.Generic;

namespace Portfolio.Application.Models.Index
{
    public class ProjectModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> Tools { get; set; }
        public string GithubLink { get; set; }
        public string CoverUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<GalleryItemModel> GalleryItems { get; set; } = new List<GalleryItemModel>();

    }
}
