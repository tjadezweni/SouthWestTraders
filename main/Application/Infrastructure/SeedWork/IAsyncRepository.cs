using System.Linq.Expressions;

namespace Application.Infrastructure.SeedWork
{
    public interface IAsyncRepository<TEntity>
        where TEntity : BaseEntity
    {
        public Task<TEntity> AddAsync(TEntity entity);

        public Task<TEntity> UpdateAsync(TEntity entity);

        public Task DeleteAsync(TEntity entity);

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

        public Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression);
    }
}
