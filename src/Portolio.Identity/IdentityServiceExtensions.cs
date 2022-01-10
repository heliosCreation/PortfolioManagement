using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Contracts.Identity;
using Portfolio.Application.Contracts.Initializer;
using Portfolio.Application.Models.Identity;
using Portfolio.Identity.Data;
using Portfolio.Identity.Services;
using System;

namespace Portfolio.Identity
{
    public static class IdentityServiceExtensions
    {
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<PortfolioIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("PortfolioIdentityDb"),
               b => b.MigrationsAssembly(typeof(PortfolioIdentityDbContext).Assembly.FullName)));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PortfolioIdentityDbContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt => {
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Password.RequiredLength = 9;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.SignIn.RequireConfirmedEmail = true;

            });
            services.AddScoped<IIdentityDbInitializer, IdentityDbInitializer>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/Login";
            });

        }
    }
}
