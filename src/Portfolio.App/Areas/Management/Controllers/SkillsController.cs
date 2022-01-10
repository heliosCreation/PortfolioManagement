using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Application.Contracts.Data;
using Portfolio.Application.ErrorParser;
using Portfolio.Application.Models.Index;
using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Areas.Management.Controllers
{
    [Area("Management")]
    public class SkillsController : BaseController
    {
        private readonly ILogger<SkillsController> _logger;
        private readonly ISkillsRepository _skillsRepository;
        private const string PartialPath = "~/Areas/Management/Views/Shared/Skills/_skillsManagementPartial.cshtml";
        private const string CreateSkillModalPath = "~/Areas/Management/Views/Shared/Skills/_createSkillModal.cshtml";
        private const string UpdateSkillModalPath = "~/Areas/Management/Views/Shared/Skills/_updateSkillModal.cshtml";
        private const string DeleteSkillModalPath = "~/Areas/Management/Views/Shared/Skills/_deleteSkillModal.cshtml";

        public SkillsController(ILogger<SkillsController> logger, ISkillsRepository skillsRepository)
        {
            _logger = logger;
            _skillsRepository = skillsRepository ?? throw new ArgumentNullException(nameof(skillsRepository));
        }
        public async Task<IActionResult> Index(bool isSuccess = false, List<string> errors = null)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.errors = errors;
            var data = await _skillsRepository.ListAllAsync();
            var model = Mapper.Map<List<SkillModel>>(data);
            return View(model);

        }

        public async Task<IActionResult> Create(SkillModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var data = Mapper.Map<Skill>(model);
            await _skillsRepository.AddAsync(data);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }

        public async Task<IActionResult> Update(SkillModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _skillsRepository.GetByIdAsync(model.Id);
            int oldPriority = retrieved.Priority;
            retrieved = Mapper.Map<Skill>(model);
            await _skillsRepository.UpdateAsync(retrieved, oldPriority);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }
        public async Task<IActionResult> Delete(Guid? Id)
        {
            if (Id is null)
            {
                var errorList = new List<string> { "There was an error trying to delete your skill. Try again later or contact an administrator." };
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _skillsRepository.GetByIdAsync(Id.Value);
            int oldPriority = retrieved.Priority;
            await _skillsRepository.DeleteAsync(retrieved, oldPriority);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }


        public async Task<IActionResult> LoadCreateModal()
        {
            var priorityOptions = await _skillsRepository.GetSkillPriorities(false);
            return PartialView(CreateSkillModalPath, new CrupdateSkillModel { PriorityOptions = priorityOptions });
        }

        public async Task<IActionResult> LoadUpdateModal(Guid id)
        {
            var data = await _skillsRepository.GetByIdAsync(id);
            var updateModel = Mapper.Map<CrupdateSkillModel>(data);
            updateModel.PriorityOptions = await _skillsRepository.GetSkillPriorities(true);
            return PartialView(UpdateSkillModalPath, updateModel);
        }
        public async Task<IActionResult> LoadDeleteModal(Guid id)
        {
            var data = await _skillsRepository.GetByIdAsync(id);
            var model = Mapper.Map<SkillModel>(data);
            return PartialView(DeleteSkillModalPath, model);
        }
    }
}