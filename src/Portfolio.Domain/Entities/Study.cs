using Portfolio.Domain.Common;
using System;

namespace Portfolio.Domain.Entities
{
    public class Study : AuditableEntity
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string SchoolName { get; set; }
        public string Diploma { get; set; }
        public string Details { get; set; }
    }
}
