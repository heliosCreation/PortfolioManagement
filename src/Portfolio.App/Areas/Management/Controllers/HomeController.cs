using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Contracts.Data;
using Portfolio.Application.Contracts.FileManagement;
using Portfolio.Application.Contracts.Infrastructure.ImageHandler;
using Portfolio.Application.ErrorParser;
using Portfolio.Application.Models.Files;
using Portfolio.Application.Models.Index;
using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Areas.Management.Controllers
{
    [Area("Management")]
    public class HomeController : BaseController
    {
        private readonly IHomeMainTextRepository _homeMainTextRepository;
        private readonly IImageHandlerService _imageHandlerService;
        private readonly IFileStorageService _fileStorageService;

        public HomeController(IHomeMainTextRepository homeMainTextRepository, IImageHandlerService imageHandlerService, IFileStorageService fileStorageService)
        {
            _homeMainTextRepository = homeMainTextRepository ?? throw new ArgumentNullException(nameof(homeMainTextRepository));
            _imageHandlerService = imageHandlerService ?? throw new ArgumentNullException(nameof(imageHandlerService));
            _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
        }
        public async Task<IActionResult> Index(bool isSuccess = false, List<string> errors = null)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.errors = errors;

            var data = await _homeMainTextRepository.GetOnly();
            var model = Mapper.Map<HomeMainDisplayModel>(data);
            return View(model);
        }

        public async Task<IActionResult> Create(HomeMainDisplayModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            FileDto fileDto = new FileDto
            {
                Content = model.CoverFile.OpenReadStream(),
                ContentType = model.CoverFile.ContentType,
                Name = model.CoverFile.FileName
            };
            var urlsDto = await _fileStorageService.UploadSingleAsync(fileDto, "Img", "Profile");

            model.CoverUrl = urlsDto.Urls.ToList()[0];
            var data = Mapper.Map<HomeMainDisplay>(model);
            await _homeMainTextRepository.AddAsync(data);

            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }
        public async Task<IActionResult> Update (HomeMainDisplayModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _homeMainTextRepository.GetOnly();
            if (model.CoverFile != null && !string.IsNullOrEmpty(model.CoverUrl))
            {
                await _fileStorageService.DeleteFileIfExistAsync("privateuploads", retrieved.CoverUrl);
            }
            FileDto fileDto = new FileDto
            {
                Content = model.CoverFile.OpenReadStream(),
                ContentType = model.CoverFile.ContentType,
                Name = model.CoverFile.FileName
            };
            var urlsDto = await _fileStorageService.UploadSingleAsync(fileDto, "Img", "Profile");
            model.CoverUrl = urlsDto.Urls.ToList()[0];
            retrieved = Mapper.Map<HomeMainDisplay>(model);
            await _homeMainTextRepository.UpdateAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });

        }

    }
}
