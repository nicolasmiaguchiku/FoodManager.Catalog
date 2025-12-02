using System.Linq.Expressions;

namespace FoodManager.Catalog.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task<long> DeleteAsync(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken);

        Task<long> ReplaceAsync(Expression<Func<TEntity, bool>> filterExpression, TEntity entity, CancellationToken cancellationToken);

        Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    }
}