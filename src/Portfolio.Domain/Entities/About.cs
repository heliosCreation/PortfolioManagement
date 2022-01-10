using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class About : AuditableEntity
    {
        public string BoldText { get; set; }
        public string MainText { get; set; }
        public string SideText { get; set; }
        public bool ShowCvLink { get; set; }
        public string CvLink { get; set; }
        public bool ShowLinkedinUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public bool ShowGithubUrl { get; set; }
        public string GithubUrl { get; set; }
    }
}
