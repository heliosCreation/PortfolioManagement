using AutoMapper;
using Portfolio.Application.Models.Index;
using Portfolio.Domain.Entities;
using System;

namespace Portfolio.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            #region homeSection
            CreateMap<HomeMainDisplay, HomeMainDisplayModel>().ReverseMap();
            #endregion

            #region AboutSection
            CreateMap<AboutModel, About>().ReverseMap();
            CreateMap<SkillModel, Skill>().ReverseMap();
            CreateMap<Skill, CrupdateSkillModel>().ReverseMap();

            CreateMap<ExperienceModel, Experience>().ReverseMap();

            CreateMap<StudyModel, Study>().ReverseMap();
            #endregion

            #region Service
            CreateMap<ServiceModel, Service>().ReverseMap();
            #endregion

            #region Project
            CreateMap<ProjectCategoryModel, ProjectCategory>().ReverseMap();
            CreateMap<GalleryItemModel, ProjectGalleryItem>().ReverseMap();
            CreateMap<CreateProjectModel, Project>();
            CreateMap<UpdateProjectModel, Project>()
                 .ForMember(dest => dest.CoverUrl, opt => opt.Condition(src => src.CoverUrl != null));
            CreateMap<Project, UpdateProjectModel>()
                .ForMember(dest => dest.ToolsString, opt => opt.MapFrom(src => String.Join(",", src.Tools)));

            CreateMap<Project, ProjectModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
            #endregion

            #region Inspiration
            CreateMap<Inspiration, InspirationModel>().ReverseMap();
            #endregion

            #region Contact
            CreateMap<Contact, ContactModel>().ReverseMap();
            #endregion
        }
    }

}
