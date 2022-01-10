using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class Inspiration : AuditableEntity
    {
        public string Quote { get; set; }
        public string Author { get; set; }
    }
}
