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
    [Area("Management")]
    public class StudyController : BaseController
    {
        private readonly IStudiesRepository _studiesRepository;
        private const string CreateModalPath = "~/Areas/Management/Views/Shared/Study/_createStudyModal.cshtml";
        private const string UpdateModalPath = "~/Areas/Management/Views/Shared/Study/_updateStudyModal.cshtml";
        private const string DeleteModalPath = "~/Areas/Management/Views/Shared/Study/_deleteStudyModal.cshtml";

        public StudyController(IStudiesRepository studiesRepository)
        {
            _studiesRepository = studiesRepository ?? throw new System.ArgumentNullException(nameof(studiesRepository));
        }

        public async Task<IActionResult> Index(bool isSuccess = false, List<string> errors = null)
        {
            var data = await _studiesRepository.ListAllAsync();
            var model = Mapper.Map<List<StudyModel>>(data);

            ViewBag.isSuccess = isSuccess;
            ViewBag.errors = errors;
            if (data.ToList().Count > 0)
            {
                ViewBag.table = TableFromObject<StudyModel>.CreateTable(model);
            }

            return View(model);
        }

        public async Task<IActionResult> Create(StudyModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var data = Mapper.Map<Study>(model);
            await _studiesRepository.AddAsync(data);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }


        public async Task<IActionResult> Update(StudyModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _studiesRepository.GetByIdAsync(model.Id);
            retrieved = Mapper.Map<Study>(model);
            await _studiesRepository.UpdateAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }

        public async Task<IActionResult> Delete(Guid? Id)
        {
            if (Id is null)
            {
                var errorList = new List<string> { "There was an error trying to delete your experience item. Try again later or contact an administrator." };
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _studiesRepository.GetByIdAsync(Id.Value);
            await _studiesRepository.DeleteAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }


        public IActionResult LoadCreateModal()
        {
            return PartialView(CreateModalPath, new StudyModel());
        }
        public async Task<IActionResult> LoadUpdateModal(Guid id)
        {
            var data = await _studiesRepository.GetByIdAsync(id);
            var model = Mapper.Map<StudyModel>(data);
            return PartialView(UpdateModalPath, model);
        }

        public async Task<IActionResult> LoadDeleteModal(Guid id)
        {
            var data = await _studiesRepository.GetByIdAsync(id);
            var model = Mapper.Map<StudyModel>(data);
            return PartialView(DeleteModalPath, model);
        }

    }
}
