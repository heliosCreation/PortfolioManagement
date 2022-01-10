using Portfolio.Application.Models.Index;
using System.Collections.Generic;

namespace Portfolio.Application.ViewModel
{
    public class ProjectsViewModel
    {
        public List<ProjectCategoryModel> ProjectCategoryListModel { get; set; } = new List<ProjectCategoryModel>();
        public List<ProjectModel> ProjectListModel { get; set; } = new List<ProjectModel>();
    }
}
