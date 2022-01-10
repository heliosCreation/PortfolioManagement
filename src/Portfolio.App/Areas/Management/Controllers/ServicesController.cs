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
    public class ServicesController : BaseController
    {
        private const string CreateModalPath = "~/Areas/Management/Views/Shared/Services/_createServiceModal.cshtml";
        private const string UpdateModalPath = "~/Areas/Management/Views/Shared/Services/_updateServiceModal.cshtml";
        private const string DeleteModalPath = "~/Areas/Management/Views/Shared/Services/_deleteServiceModal.cshtml";

        private readonly IServicesRepository _servicesRepository;

        public ServicesController(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository ?? throw new System.ArgumentNullException(nameof(servicesRepository));
        }
        public async Task<IActionResult> Index(bool isSuccess = false, List<string> errors = null)
        {

            var data = await _servicesRepository.ListAllAsync();
            var model = Mapper.Map<List<ServiceModel>>(data);

            ViewBag.isSuccess = isSuccess;
            ViewBag.errors = errors;

            if (data.ToList().Count > 0)
            {
                ViewBag.table = TableFromObject<ServiceModel>.CreateTable(model);
            }

            return View(model);
        }

        public async Task<IActionResult> Create(ServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var data = Mapper.Map<Service>(model);
            await _servicesRepository.AddAsync(data);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }
        public async Task<IActionResult> Update(ServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _servicesRepository.GetByIdAsync(model.Id);
            retrieved = Mapper.Map<Service>(model);
            await _servicesRepository.UpdateAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }

        public async Task<IActionResult> Delete(Guid? Id)
        {
            if (Id is null)
            {
                var errorList = new List<string> { "There was an error trying to delete your service item. Try again later or contact an administrator." };
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _servicesRepository.GetByIdAsync(Id.Value);
            await _servicesRepository.DeleteAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }
        public IActionResult LoadCreateModal()
        {
            return PartialView(CreateModalPath, new ServiceModel());
        }

        public async Task<IActionResult> LoadUpdateModal(Guid id)
        {
            var data = await _servicesRepository.GetByIdAsync(id);
            var model = Mapper.Map<ServiceModel>(data);
            return PartialView(UpdateModalPath, model);
        }

        public async Task<IActionResult> LoadDeleteModal(Guid id)
        {
            var data = await _servicesRepository.GetByIdAsync(id);
            var model = Mapper.Map<ServiceModel>(data);
            return PartialView(DeleteModalPath, model);
        }
    }
}
