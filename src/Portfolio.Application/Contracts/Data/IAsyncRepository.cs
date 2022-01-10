using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Application.Contracts.Data
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetOnly();
        Task<IEnumerable<T>> ListAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task UpdateOnlyAsync(T entity);
        Task DeleteAsync(T entity);

    }
}
