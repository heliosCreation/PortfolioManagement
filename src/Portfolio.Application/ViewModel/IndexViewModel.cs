using Portfolio.Application.Models.Index;
using System.Collections.Generic;

namespace Portfolio.Application.ViewModel
{
    public class IndexViewModel
    {
        public HomeMainDisplayModel HomeMainTextModel { get; set; }
        public AboutViewModel AboutVm { get; set; } = new AboutViewModel();
        public List<ServiceModel> ServicesListModel { get; set; }
        public ProjectsViewModel ProjectVm { get; set; } = new ProjectsViewModel();
        public List<InspirationModel> InspirationListModel { get; set; }
        public ContactModel Contact { get; set; }
    }
}
