using Portfolio.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Portfolio.Application.Contracts.Data
{
    public interface IProjectRepository : IAsyncRepository<Project>
    {
        Task DeleteAssociatedGalleryItem(Guid id);
    }
}
