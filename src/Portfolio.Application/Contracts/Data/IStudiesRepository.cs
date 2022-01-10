using Portfolio.Domain.Entities;

namespace Portfolio.Application.Contracts.Data
{
    public interface IStudiesRepository : IAsyncRepository<Study>
    {
    }
}
