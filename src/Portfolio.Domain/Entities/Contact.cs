using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class Contact : AuditableEntity
    {
        public string Email { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
    }
}
