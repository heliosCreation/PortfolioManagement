using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Portfolio.Application.Contracts.Initializer;
using Portfolio.Application.Models.Identity;
using Portfolio.Identity.Data;
using System;
using System.Linq;

namespace Portfolio.Identity.Services
{
    public class IdentityDbInitializer : IIdentityDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<IdentityDbInitializer> _logger;

        public IdentityDbInitializer(IServiceScopeFactory scopeFactory, ILogger<IdentityDbInitializer> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<PortfolioIdentityDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedRoles()
        {
            _logger.LogInformation("Role seeding started.");
            try
            {
                using (var serviceScope = _scopeFactory.CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<PortfolioIdentityDbContext>())
                    {
                        //add admin user
                        if (!context.Roles.Any())
                        {
                            var role = new IdentityRole
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "Admin",
                                NormalizedName = "Admin"
                            };

                            context.Roles.Add(role);
                        }


                        context.SaveChanges();
                        _logger.LogInformation("Role created");
                    }
                }

            }
            catch (Exception)
            {
                _logger.LogError("There was en error trying to add the role to the identity database.");
            }
        }

        private string PassGenerate(ApplicationUser user, string password)
        {
            var passHash = new PasswordHasher<ApplicationUser>();
            return passHash.HashPassword(user, password);
        }
    }
}
