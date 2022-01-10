using Portfolio.Domain.Common;
using System;

namespace Portfolio.Domain.Entities
{
    public class Experience : AuditableEntity
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string CompanyName { get; set; }
        public string PositionName { get; set; }
        public string Details { get; set; }
    }
}
