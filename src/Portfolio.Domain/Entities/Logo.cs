using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class Logo : AuditableEntity
    {
        public string Text { get; set; }
    }
}
