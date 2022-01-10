using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Application.Contracts.Data;
using Portfolio.Application.ErrorParser;
using Portfolio.Application.Models.Index;
using Portfolio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Areas.Management.Controllers
{
    public class ContactController : BaseController
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactRepository _contactRepository;

        public ContactController(ILogger<ContactController> logger, IContactRepository contactRepository)
        {
            _logger = logger;
            _contactRepository = contactRepository ?? throw new System.ArgumentNullException(nameof(contactRepository));
        }

        public async Task<IActionResult> Index(bool isSuccess = false, List<string> errors = null)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.errors = errors;
            var data = await _contactRepository.GetOnly();
            var model = Mapper.Map<ContactModel>(data);
            return View(model);
        }

        public async Task<IActionResult> Crupdate(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }
            if (model.Id == null)
            {
                var data = Mapper.Map<Contact>(model);
                await _contactRepository.AddAsync(data);
                return RedirectToAction(nameof(Index), new { isSuccess = true });
            }
            var retrieved = await _contactRepository.GetOnly();
            retrieved = Mapper.Map<Contact>(model);
            await _contactRepository.UpdateAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }

    }
}
