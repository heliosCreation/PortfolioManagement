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
    public class AboutController : BaseController
    {
        private readonly ILogger<AboutController> _logger;
        private readonly IAboutSectionRepository _aboutSectionRepository;
        private const string PartialPath = "~/Areas/Management/Views/Shared/Partial/_aboutManagementPartial.cshtml";

        public AboutController(ILogger<AboutController> logger, IAboutSectionRepository aboutSectionRepository)
        {
            _logger = logger;
            _aboutSectionRepository = aboutSectionRepository ?? throw new System.ArgumentNullException(nameof(aboutSectionRepository));
        }

        public async Task<IActionResult> Index(bool isSuccess = false, List<string> errors = null)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.errors = errors;
            var data = await _aboutSectionRepository.GetOnly();
            var model = Mapper.Map<AboutModel>(data);
            return View(model);
        }

        public async Task<IActionResult> Update(AboutModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }
            if (model.Id == null)
            {
                model.Id = Guid.NewGuid();
                var data = Mapper.Map<About>(model);
                await _aboutSectionRepository.AddAsync(data);
                return RedirectToAction(nameof(Index), new { isSuccess = true });
            }

            var retrieved = await _aboutSectionRepository.GetOnly();
            retrieved = Mapper.Map<About>(model);
            await _aboutSectionRepository.UpdateAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }

    }
}
