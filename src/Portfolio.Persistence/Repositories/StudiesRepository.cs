using Portfolio.Application.Contracts.Data;
using Portfolio.Domain.Entities;

namespace Portfolio.Persistence.Repositories
{
    public class StudiesRepository : BaseRepository<Study>, IStudiesRepository
    {
        public StudiesRepository(PortfolioDbContext dbContext) : base(dbContext)
        {
        }
    }
}
