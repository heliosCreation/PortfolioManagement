using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Contracts.Data;
using Portfolio.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Portfolio.Persistence.Repositories
{
    public class ProjectCategoryRepository : BaseRepository<ProjectCategory>, IProjectCategoriesRepository
    {

        public ProjectCategoryRepository(PortfolioDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsLinkedToAProject(Guid id)
        {
            return await _dbContext.Projects.AnyAsync(p => p.CategoryId == id);
        }
    }
}
