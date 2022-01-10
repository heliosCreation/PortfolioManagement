using Portfolio.Application.Contracts.Data;
using Portfolio.Domain.Entities;

namespace Portfolio.Persistence.Repositories
{
    public class OwnerPresentationRepository : BaseRepository<Logo>, IOwnerPresentationRepository
    {
        public OwnerPresentationRepository(PortfolioDbContext dbContext) : base(dbContext)
        {
        }
    }
}
