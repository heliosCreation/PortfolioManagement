﻿using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Contracts.Data;
using Portfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : AuditableEntity
    {
        protected readonly PortfolioDbContext _dbContext;

        public BaseRepository(PortfolioDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<T> GetOnly()
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateOnlyAsync(T entity)
        {
            var target = await _dbContext.Set<T>().FirstOrDefaultAsync();
            target = entity;
            await _dbContext.SaveChangesAsync();
        }
    }
}
