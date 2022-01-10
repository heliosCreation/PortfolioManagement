using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Models.Identity;
using System.Reflection;

namespace Portfolio.Identity.Data
{
    public class PortfolioIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public PortfolioIdentityDbContext(DbContextOptions<PortfolioIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
