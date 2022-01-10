using Portfolio.Application.Contracts.Data;
using Portfolio.Domain.Entities;

namespace Portfolio.Persistence.Repositories
{
    public class InspirationsRepository : BaseRepository<Inspiration>, IInspirationsRepository
    {

        public InspirationsRepository(PortfolioDbContext dbContext) : base(dbContext)
        {
        }
    }
}
