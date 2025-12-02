using FoodManager.Catalog.Domain.Entities;
using FoodManager.Catalog.Domain.Filters;
using System.Linq.Expressions;

namespace FoodManager.Catalog.Domain.Interfaces.Repositories;

public interface IFoodRepository : IBaseRepository<FoodEntity>
{
    Task<PagedResult<FoodEntity>> GetFoodsAsync(FoodFiltersBuilder filters, CancellationToken cancellationToken);
}