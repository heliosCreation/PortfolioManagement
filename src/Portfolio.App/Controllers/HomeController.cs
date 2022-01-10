using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Application.Contracts;
using Portfolio.Application.Contracts.Data;
using Portfolio.Application.Models;
using Portfolio.Application.Models.Errors;
using Portfolio.Application.Models.Index;
using Portfolio.Application.ViewModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeMainTextRepository _homeMainTextRepository;
        private readonly IAboutSectionRepository _aboutSectionRepository;
        private readonly ISkillsRepository _skillsRepository;
        private readonly IExperienceRepository _experienceRepository;
        private readonly IStudiesRepository _studiesRepository;
        private readonly IServicesRepository _servicesRepository;
        private readonly IProjectCategoriesRepository _projectCategoriesRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IInspirationsRepository _inspirationsRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,
            IHomeMainTextRepository homeMainTextRepository,
            IAboutSectionRepository aboutSectionRepository,
            ISkillsRepository skillsRepository,
            IExperienceRepository experienceRepository,
            IStudiesRepository studiesRepository,
            IServicesRepository servicesRepository,
            IProjectCategoriesRepository projectCategoriesRepository,
            IProjectRepository projectRepository,
            IInspirationsRepository inspirationsRepository,
            IContactRepository contactRepository,
            IMapper mapper)
        {
            _logger = logger;
            _homeMainTextRepository = homeMainTextRepository ?? throw new System.ArgumentNullException(nameof(homeMainTextRepository));
            _aboutSectionRepository = aboutSectionRepository ?? throw new System.ArgumentNullException(nameof(aboutSectionRepository));
            _skillsRepository = skillsRepository ?? throw new System.ArgumentNullException(nameof(skillsRepository));
            _experienceRepository = experienceRepository ?? throw new System.ArgumentNullException(nameof(experienceRepository));
            _studiesRepository = studiesRepository ?? throw new System.ArgumentNullException(nameof(studiesRepository));
            _servicesRepository = servicesRepository ?? throw new System.ArgumentNullException(nameof(servicesRepository));
            _projectCategoriesRepository = projectCategoriesRepository ?? throw new System.ArgumentNullException(nameof(projectCategoriesRepository));
            _projectRepository = projectRepository ?? throw new System.ArgumentNullException(nameof(projectRepository));
            _inspirationsRepository = inspirationsRepository ?? throw new System.ArgumentNullException(nameof(inspirationsRepository));
            _contactRepository = contactRepository ?? throw new System.ArgumentNullException(nameof(contactRepository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<IActionResult> Index()
        {
            var vm = new IndexViewModel();

            var homeText = await _homeMainTextRepository.GetOnly();
            var about = await _aboutSectionRepository.GetOnly();
            var skills = await _skillsRepository.ListAllAsync();
            var experience = await _experienceRepository.ListAllAsync();
            var studies = await _studiesRepository.ListAllAsync();
            var services = await _servicesRepository.ListAllAsync();
            var projectCategories = await _projectCategoriesRepository.ListAllAsync();
            var projects = await _projectRepository.ListAllAsync();
            var inspirations = await _inspirationsRepository.ListAllAsync();
            var contact = await _contactRepository.GetOnly();

            vm.HomeMainTextModel = _mapper.Map<HomeMainDisplayModel>(homeText);

            vm.AboutVm.AboutModel = about != null ? _mapper.Map<AboutModel>(about) : new AboutModel();
            vm.AboutVm.AboutModel.CoverUrl = vm.HomeMainTextModel == null ? "" : vm.HomeMainTextModel.CoverUrl;
            vm.AboutVm.SkillListModel = _mapper.Map<List<SkillModel>>(skills);
            vm.AboutVm.ExperienceListModel = _mapper.Map<List<ExperienceModel>>(experience);
            vm.AboutVm.StudyListModel = _mapper.Map<List<StudyModel>>(studies);
            vm.ServicesListModel = _mapper.Map<List<ServiceModel>>(services);
            vm.ProjectVm.ProjectCategoryListModel = _mapper.Map<List<ProjectCategoryModel>>(projectCategories);
            vm.ProjectVm.ProjectListModel = _mapper.Map<List<ProjectModel>>(projects);
            vm.InspirationListModel = _mapper.Map<List<InspirationModel>>(inspirations);
            vm.Contact = _mapper.Map<ContactModel>(contact);

            ViewBag.Logo = vm.HomeMainTextModel == null ? "H" : vm.HomeMainTextModel.Logo;
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}