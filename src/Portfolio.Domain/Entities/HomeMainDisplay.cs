using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class HomeMainDisplay : AuditableEntity
    {
        public string Logo { get; set; }
        public string p { get; set; }
        public string h1 { get; set; }
        public string h4 { get; set; }
        public string CoverUrl { get; set; }
    }
}
