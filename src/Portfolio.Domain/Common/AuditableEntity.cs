using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Common
{
    public class AuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
