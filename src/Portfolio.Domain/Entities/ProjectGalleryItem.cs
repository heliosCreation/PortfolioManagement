using Portfolio.Domain.Common;
using System;

namespace Portfolio.Domain.Entities
{
    public class ProjectGalleryItem : AuditableEntity
    {
        public string Url { get; set; }
        public Project Project { get; set; }
        public Guid ProjectId { get; set; }
    }
}
