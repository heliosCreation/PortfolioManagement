using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class ProjectCategory : AuditableEntity
    {
        public string Name { get; set; }
    }
}
