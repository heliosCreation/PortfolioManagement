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
    public class ProjectController : BaseController
    {
        private const string CreateModalPath = "~/Areas/Management/Views/Shared/Project/_createProjectModal.cshtml";
        private const string UpdateModalPath = "~/Areas/Management/Views/Shared/Project/_updateProjectModal.cshtml";
        private const string DeleteModalPath = "~/Areas/Management/Views/Shared/Project/_deleteProjectModal.cshtml";

        private readonly IProjectRepository _projectRepository;
        private readonly IProjectCategoriesRepository _projectCategoriesRepository;
        private readonly IImageHandlerService _imageHandlerService;
        private readonly IFileStorageService _fileStorageService;

        public ProjectController(IProjectRepository projectRepository,
            IProjectCategoriesRepository projectCategoriesRepository,
            IImageHandlerService imageHandlerService,
            IFileStorageService fileStorageService)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _projectCategoriesRepository = projectCategoriesRepository ?? throw new ArgumentNullException(nameof(projectCategoriesRepository));
            _imageHandlerService = imageHandlerService ?? throw new ArgumentNullException(nameof(imageHandlerService));
            _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
        }
        public async Task<IActionResult> Index(bool isSuccess = false, List<string> errors = null)
        {
            var categories = await _projectCategoriesRepository.ListAllAsync();
            if (categories.ToList().Count < 1)
            {
                errors.Add("Please consider creating a project category before creating a project.");
            }
            ViewBag.isSuccess = isSuccess;
            ViewBag.errors = errors;

            var data = await _projectRepository.ListAllAsync();
            var model = Mapper.Map<List<ProjectModel>>(data);
            return View(model);
        }

        public async Task<IActionResult> Create(CreateProjectModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CoverFile != null)
                {
                    FileDto fileDto = new FileDto
                    {
                        Content = model.CoverFile.OpenReadStream(),
                        ContentType = model.CoverFile.ContentType,
                        Name = model.CoverFile.FileName
                    };
                    var urlsDto = await _fileStorageService.UploadSingleAsync(fileDto, "Img", "Project/Cover");
                    model.CoverUrl = urlsDto.Urls.ToList()[0];
                }

                if (model.GalleryFiles != null)
                {
                    foreach (var image in model.GalleryFiles)
                    {
                        FileDto fileDto = new FileDto
                        {
                            Content = image.OpenReadStream(),
                            ContentType = image.ContentType,
                            Name = image.FileName
                        };

                        var urlDto = await _fileStorageService.UploadSingleAsync(fileDto, "Img", "Project/Gallery");
                        var galleryItem = new GalleryItemModel
                        {
                            Url = urlDto.Urls.ToList()[0]
                        };

                        model.GalleryItems.Add(galleryItem);
                    }
                }
                model.CategoryModel = new ProjectCategoryModel { Id = model.CategoryId }; ;
                var data = Mapper.Map<Project>(model);
                await _projectRepository.AddAsync(data);
                return RedirectToAction(nameof(Index), new { isSuccess = true });
            }
            var errorList = ErrorParser.Parse(ModelState);
            return RedirectToAction(nameof(Index), new { errors = errorList });


        }
        public async Task<IActionResult> Update(UpdateProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ErrorParser.Parse(ModelState);
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }
            var retrieved = await _projectRepository.GetByIdAsync(model.Id);

            if (model.CoverFile != null)
            {
                if (!string.IsNullOrEmpty(model.CoverUrl))
                {
                    await _fileStorageService.DeleteFileIfExistAsync("privateuploads", retrieved.CoverUrl);
                }
                FileDto fileDto = new FileDto
                {
                    Content = model.CoverFile.OpenReadStream(),
                    ContentType = model.CoverFile.ContentType,
                    Name = model.CoverFile.FileName
                };

                var urlDto = await _fileStorageService.UploadSingleAsync(fileDto, "Img", "Project/Cover");
                model.CoverUrl = urlDto.Urls.ToList()[0];
            }

            if (model.GalleryFiles != null)
            {
                foreach (var image in model.GalleryFiles)
                {
                    FileDto fileDto = new FileDto
                    {
                        Content = image.OpenReadStream(),
                        ContentType = image.ContentType,
                        Name = image.FileName
                    };

                    var urlDto = await _fileStorageService.UploadSingleAsync(fileDto, "Img", "Project/Gallery");
                    var galleryItem = new GalleryItemModel
                    {
                        Url = urlDto.Urls.ToList()[0]
                    };

                    model.GalleryItems.Add(galleryItem);
                }
            }

            foreach (var galleryItem in model.GalleryItems.ToList())
            {
                if (galleryItem.ShouldBeDeleted)
                {
                    await _fileStorageService.DeleteFileIfExistAsync("privateuploads",galleryItem.Url);
                    await _projectRepository.DeleteAssociatedGalleryItem(galleryItem.Id);
                    model.GalleryItems.Remove(galleryItem);
                }
            }

            retrieved = Mapper.Map<Project>(model);
            await _projectRepository.UpdateAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }
        public async Task<IActionResult> Delete(Guid? Id)
        {
            if (Id is null)
            {
                var errorList = new List<string> { "There was an error trying to delete your project. Try again later or contact an administrator." };
                return RedirectToAction(nameof(Index), new { errors = errorList });
            }

            var retrieved = await _projectRepository.GetByIdAsync(Id.Value);

            foreach (var item in retrieved.GalleryItems)
            {
                await _fileStorageService.DeleteFileIfExistAsync("privateuploads",item.Url);
            }
            await _fileStorageService.DeleteFileIfExistAsync("privateuploads", retrieved.CoverUrl);
            await _projectRepository.DeleteAsync(retrieved);
            return RedirectToAction(nameof(Index), new { isSuccess = true });
        }
        public async Task<IActionResult> LoadCreateModal()
        {
            var model = new CreateProjectModel();
            var categoriesData = await _projectCategoriesRepository.ListAllAsync();
            model.Categories = Mapper.Map<List<ProjectCategoryModel>>(categoriesData);
            return PartialView(CreateModalPath, model);
        }

        public async Task<IActionResult> LoadUpdateModal(Guid id)
        {
            var data = await _projectRepository.GetByIdAsync(id);
            var model = Mapper.Map<UpdateProjectModel>(data);
            var categoriesData = await _projectCategoriesRepository.ListAllAsync();
            model.Categories = Mapper.Map<List<ProjectCategoryModel>>(categoriesData);
            return PartialView(UpdateModalPath, model);
        }

        public async Task<IActionResult> LoadDeleteModal(Guid id)
        {
            var data = await _projectRepository.GetByIdAsync(id);
            var model = Mapper.Map<ProjectModel>(data);
            return PartialView(DeleteModalPath, model);
        }
    }
}
