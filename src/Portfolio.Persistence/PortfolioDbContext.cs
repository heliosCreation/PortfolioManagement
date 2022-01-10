using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Common;
using Portfolio.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Persistence
{
    public class PortfolioDbContext : DbContext
    {

        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
           : base(options)
        {
        }


        public DbSet<HomeMainDisplay> HomeMainTexts { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Study> Studies { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<ProjectGalleryItem> ProjectsGalleryItems { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Inspiration> Inspirations { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortfolioDbContext).Assembly);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}