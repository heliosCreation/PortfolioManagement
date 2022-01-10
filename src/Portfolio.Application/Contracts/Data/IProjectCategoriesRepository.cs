using Portfolio.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Portfolio.Application.Contracts.Data
{
    public interface IProjectCategoriesRepository : IAsyncRepository<ProjectCategory>
    {
        Task<bool> IsLinkedToAProject(Guid id);
    }
}
