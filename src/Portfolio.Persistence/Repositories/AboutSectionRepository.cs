using Portfolio.Application.Contracts.Data;
using Portfolio.Domain.Entities;

namespace Portfolio.Persistence.Repositories
{
    public class AboutSectionRepository : BaseRepository<About>, IAboutSectionRepository
    {

        public AboutSectionRepository(PortfolioDbContext dbContext) : base(dbContext)
        {
        }
    }
}
