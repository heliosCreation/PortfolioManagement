using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Contracts.Data;
using Portfolio.Application.ErrorParser;
using Portfolio.Application.Models.Index;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.HtmlFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Areas.Management.Controllers
{
    public class ProjectCategoryController : BaseController
    {
        private const string CreateModalPath = "~/Areas/Management/Views/Shared/ProjectCategory/_createProjectCategoryModal.cshtml";
        private const string UpdateModalPath = "~/Areas/Management/Views/Shared/ProjectCategory/_updateProjectCategoryModal.cshtml";
        private const string DeleteModalPath = "~/Areas/Management/Views/Shared/ProjectCategory/_deleteProjectCategoryModal.cshtml";

        private readonly IProjectCategoriesRepository _projectCategoryRepository;

        public ProjectCategoryController(IProjectCategoriesRepository projectCategoryRepository)
        {
            _projectCategoryRepository = projectCategoryRepository ?? throw new ArgumentNullException(nameof(projectCategoryRepository));
        }

        public async Task<IActionResult> Index(bool isSuccess = false, List<string> errors = null)
        {

            var data = await _projectCategoryRepository.ListAllAsync();
            var model = Mapper.Map<List<ProjectCategoryModel>>(data);

            ViewBag.isSuccess = isSuccess;
            ViewBag.errors = errors;
            if (data.ToList().Count > 0)
            {
                ViewBag.table = TableFromObject<ProjectCategoryModel>.CreateTable(model);
            }

            return View(model);
        }

        public async Task<IActionResult> Create(ProjectCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var data = Mapper.Map<ProjectCategory>(model);
            await _projectCategoryRepository.AddAsync(data);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }
        public async Task<IActionResult> Update(ProjectCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _projectCategoryRepository.GetByIdAsync(model.Id);
            retrieved = Mapper.Map<ProjectCategory>(model);
            await _projectCategoryRepository.UpdateAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }

        public async Task<IActionResult> Delete(Guid? Id)
        {
            if (Id is null)
            {
                var errorList = new List<string> { "There was an error trying to delete your service item. Try again later or contact an administrator." };
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }
            if (await _projectCategoryRepository.IsLinkedToAProject(Id.Value))
            {
                var errors = new List<string> { "This category is currently used by one or many project. Unlink them before deleting this category." };
                return RedirectToAction(nameof(Index), new { isSuccess = false, errors = errors });
            }
            var retrieved = await _projectCategoryRepository.GetByIdAsync(Id.Value);
            await _projectCategoryRepository.DeleteAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }
        public IActionResult LoadCreateModal()
        {
            return PartialView(CreateModalPath, new ProjectCategoryModel());
        }

        public async Task<IActionResult> LoadUpdateModal(Guid id)
        {
            var data = await _projectCategoryRepository.GetByIdAsync(id);
            var model = Mapper.Map<ProjectCategoryModel>(data);
            return PartialView(UpdateModalPath, model);
        }

        public async Task<IActionResult> LoadDeleteModal(Guid id)
        {
            var data = await _projectCategoryRepository.GetByIdAsync(id);
            var model = Mapper.Map<ProjectCategoryModel>(data);
            return PartialView(DeleteModalPath, model);
        }
    }
}
