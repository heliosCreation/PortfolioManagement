using Portfolio.Domain.Common;
using System;
using System.Collections.Generic;

namespace Portfolio.Domain.Entities
{
    public class Project : AuditableEntity
    {
        public ProjectCategory Category { get; set; } = new ProjectCategory();
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tools { get; set; }
        public string GithubLink { get; set; }
        public string CoverUrl { get; set; }
        public List<ProjectGalleryItem> GalleryItems { get; set; }
    }
}
