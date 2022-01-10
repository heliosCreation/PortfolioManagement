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
    public class ExperienceController : BaseController
    {
        public ExperienceController(IExperienceRepository experienceRepository)
        {
            _experienceRepository = experienceRepository ?? throw new System.ArgumentNullException(nameof(experienceRepository));
        }

        private const string CreateExperienceModalPath = "~/Areas/Management/Views/Shared/Experience/_createExperienceModal.cshtml";
        private const string UpdateExperienceModalPath = "~/Areas/Management/Views/Shared/Experience/_updateExperienceModal.cshtml";
        private const string DeleteExperienceModalPath = "~/Areas/Management/Views/Shared/Experience/_deleteExperienceModal.cshtml";
        private readonly IExperienceRepository _experienceRepository;

        public async Task<IActionResult> Index(bool isSuccess = false, List<string> errors = null)
        {
            var data = await _experienceRepository.ListAllAsync();
            var model = Mapper.Map<List<ExperienceModel>>(data);

            ViewBag.isSuccess = isSuccess;
            ViewBag.errors = errors;
            if (data.ToList().Count > 0)
            {
                ViewBag.table = TableFromObject<ExperienceModel>.CreateTable(model);
            }

            return View(model);
        }

        public async Task<IActionResult> Create(ExperienceModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var data = Mapper.Map<Experience>(model);
            await _experienceRepository.AddAsync(data);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }

        public async Task<IActionResult> Update(ExperienceModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _experienceRepository.GetByIdAsync(model.Id);
            retrieved = Mapper.Map<Experience>(model);
            await _experienceRepository.UpdateAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }

        public async Task<IActionResult> Delete(Guid? Id)
        {
            if (Id is null)
            {
                var errorList = new List<string> { "There was an error trying to delete your experience item. Try again later or contact an administrator." };
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _experienceRepository.GetByIdAsync(Id.Value);
            await _experienceRepository.DeleteAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }

        public IActionResult LoadCreateModal()
        {
            return PartialView(CreateExperienceModalPath, new ExperienceModel());
        }
        public async Task<IActionResult> LoadUpdateModal(Guid id)
        {
            var data = await _experienceRepository.GetByIdAsync(id);
            var model = Mapper.Map<ExperienceModel>(data);
            return PartialView(UpdateExperienceModalPath, model);
        }

        public async Task<IActionResult> LoadDeleteModal(Guid id)
        {
            var data = await _experienceRepository.GetByIdAsync(id);
            var model = Mapper.Map<ExperienceModel>(data);
            return PartialView(DeleteExperienceModalPath, model);
        }
    }
}
