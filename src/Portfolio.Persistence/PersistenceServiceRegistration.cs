using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Contracts.Data;
using Portfolio.Persistence.Repositories;

namespace Portfolio.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<PortfolioDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("PortfolioDb"),
               b => b.MigrationsAssembly(typeof(PortfolioDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IHomeMainTextRepository, HomeMainTextRepository>();
            services.AddScoped<IAboutSectionRepository, AboutSectionRepository>();
            services.AddScoped<ISkillsRepository, SkillRepository>();
            services.AddScoped<IExperienceRepository, ExperienceRepository>();
            services.AddScoped<IStudiesRepository, StudiesRepository>();
            services.AddScoped<IServicesRepository, ServicesRepository>();
            services.AddScoped<IProjectCategoriesRepository, ProjectCategoryRepository>();
            services.AddScoped<IProjectRepository, ProjectsRepository>();
            services.AddScoped<IInspirationsRepository, InspirationsRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IOwnerPresentationRepository, OwnerPresentationRepository>();
            return services;
        }
    }
}
