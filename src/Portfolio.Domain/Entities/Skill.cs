using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class Skill : AuditableEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Priority { get; set; }
    }
}
