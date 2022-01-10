using Portfolio.Application.Contracts.Data;
using Portfolio.Domain.Entities;

namespace Portfolio.Persistence.Repositories
{
    public class HomeMainTextRepository : BaseRepository<HomeMainDisplay>, IHomeMainTextRepository
    {

        public HomeMainTextRepository(PortfolioDbContext dbContext) : base(dbContext)
        {
        }
    }
}
