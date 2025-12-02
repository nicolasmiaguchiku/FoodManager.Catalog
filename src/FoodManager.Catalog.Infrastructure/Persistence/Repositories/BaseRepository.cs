using DnsClient.Internal;
using FoodManager.Catalog.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace FoodManager.Catalog.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<TEntity>(IMongoDatabase mongoDb, string collectionName, ILogger<BaseRepository<TEntity>> _logger) : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _collection = mongoDb.GetCollection<TEntity>(collectionName);

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
             await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task<long> DeleteAsync(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken)
        {
            var result = await _collection.DeleteOneAsync(filterExpression, cancellationToken: cancellationToken);

            return result.DeletedCount;
        }

        public async Task<long> ReplaceAsync(Expression<Func<TEntity, bool>> filterExpression, TEntity entity, CancellationToken cancellationToken)
        {
            var result = await _collection.ReplaceOneAsync(filterExpression, entity, cancellationToken: cancellationToken);

            if (result.ModifiedCount >= 1)
            {
                _logger.LogInformation("Document updated successfully from collection {CollectionName}.", typeof(TEntity).Name);
            }

            return result.ModifiedCount;
        }

        public async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var result = await _collection.Find(predicate).FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}