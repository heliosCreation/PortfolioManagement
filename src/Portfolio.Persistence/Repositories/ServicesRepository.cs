using Portfolio.Application.Contracts.Data;
using Portfolio.Domain.Entities;

namespace Portfolio.Persistence.Repositories
{
    public class ServicesRepository : BaseRepository<Service>, IServicesRepository
    {
        public ServicesRepository(PortfolioDbContext dbContext) : base(dbContext)
        {
        }
    }
}
