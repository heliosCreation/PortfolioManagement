using Portfolio.Application.Contracts.Data;
using Portfolio.Domain.Entities;

namespace Portfolio.Persistence.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {

        public ContactRepository(PortfolioDbContext dbContext) : base(dbContext)
        {
        }
    }
}
