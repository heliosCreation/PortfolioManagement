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
    public class InspirationController : BaseController
    {
        private const string CreateModalPath = "~/Areas/Management/Views/Shared/Inspiration/_createInspirationModal.cshtml";
        private const string UpdateModalPath = "~/Areas/Management/Views/Shared/Inspiration/_updateInspirationModal.cshtml";
        private const string DeleteModalPath = "~/Areas/Management/Views/Shared/Inspiration/_deleteInspirationModal.cshtml";

        private readonly IInspirationsRepository _inspirationsRepository;

        public InspirationController(IInspirationsRepository inspirationsRepository)
        {
            _inspirationsRepository = inspirationsRepository ?? throw new System.ArgumentNullException(nameof(inspirationsRepository));
        }
        public async Task<IActionResult> Index(bool isSuccess = false, List<string> errors = null)
        {
            var data = await _inspirationsRepository.ListAllAsync();
            var model = Mapper.Map<List<InspirationModel>>(data);

            ViewBag.isSuccess = isSuccess;
            ViewBag.errors = errors;

            if (data.ToList().Count > 0)
            {
                ViewBag.table = TableFromObject<InspirationModel>.CreateTable(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Create(InspirationModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var data = Mapper.Map<Inspiration>(model);
            await _inspirationsRepository.AddAsync(data);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }
        public async Task<IActionResult> Update(InspirationModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _inspirationsRepository.GetByIdAsync(model.Id);
            retrieved = Mapper.Map<Inspiration>(model);
            await _inspirationsRepository.UpdateAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }

        public async Task<IActionResult> Delete(Guid? Id)
        {
            if (Id is null)
            {
                var errorList = new List<string> { "There was an error trying to delete your inspiration item. Try again later or contact an administrator." };
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _inspirationsRepository.GetByIdAsync(Id.Value);
            await _inspirationsRepository.DeleteAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }
        public IActionResult LoadCreateModal()
        {
            return PartialView(CreateModalPath, new InspirationModel());
        }

        public async Task<IActionResult> LoadUpdateModal(Guid id)
        {
            var data = await _inspirationsRepository.GetByIdAsync(id);
            var model = Mapper.Map<InspirationModel>(data);
            return PartialView(UpdateModalPath, model);
        }

        public async Task<IActionResult> LoadDeleteModal(Guid id)
        {
            var data = await _inspirationsRepository.GetByIdAsync(id);
            var model = Mapper.Map<InspirationModel>(data);
            return PartialView(DeleteModalPath, model);
        }
    }
}
