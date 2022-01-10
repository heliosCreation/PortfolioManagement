using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Contracts.Data;
using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Persistence.Repositories
{
    public class ProjectsRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectsRepository(PortfolioDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Project> AddAsync(Project entity)
        {
            var category = await _dbContext.ProjectCategories.FirstOrDefaultAsync(c => c.Id == entity.CategoryId);
            entity.Category = category;
            return await base.AddAsync(entity);
        }
        public override async Task<Project> GetByIdAsync(Guid id)
        {
            return await _dbContext.Projects
                .Include(p => p.GalleryItems)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public override async Task<IEnumerable<Project>> ListAllAsync()
        {
            var @base = await _dbContext.Projects
                .Include(p => p.GalleryItems)
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in @base)
            {
                var category = await _dbContext.ProjectCategories.FindAsync(item.CategoryId);
                item.Category.Name = category.Name;
            }

            return @base;
        }

        public override async Task UpdateAsync(Project entity)
        {
            foreach (var galleryItem in entity.GalleryItems)
            {
                if (galleryItem.Id == Guid.Empty)
                {
                    await _dbContext.ProjectsGalleryItems.AddAsync(new ProjectGalleryItem
                    {
                        Url = galleryItem.Url,
                        ProjectId = entity.Id
                    });
                }
            }
            await base.UpdateAsync(entity);

        }

        public async Task DeleteAssociatedGalleryItem(Guid id)
        {
            var project =
                 await _dbContext
                .ProjectsGalleryItems
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            _dbContext.Set<ProjectGalleryItem>().Remove(project);
            await _dbContext.SaveChangesAsync();
        }
    }
}
