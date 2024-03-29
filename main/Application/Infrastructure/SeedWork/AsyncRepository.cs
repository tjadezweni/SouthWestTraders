﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Infrastructure.SeedWork
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly SouthWestTradersDBContext _dbContext;

        public AsyncRepository(SouthWestTradersDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AsNoTracking().Where(expression).ToListAsync();
        }
    }
}
