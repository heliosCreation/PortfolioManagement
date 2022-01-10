using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class Service : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
