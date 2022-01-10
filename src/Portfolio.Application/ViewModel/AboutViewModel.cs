using Portfolio.Application.Models.Index;
using System.Collections.Generic;

namespace Portfolio.Application.ViewModel
{
    public class AboutViewModel
    {
        public AboutModel AboutModel { get; set; }
        public List<SkillModel> SkillListModel { get; set; }
        public List<ExperienceModel> ExperienceListModel { get; set; }

        public List<StudyModel> StudyListModel { get; set; }
    }
}
