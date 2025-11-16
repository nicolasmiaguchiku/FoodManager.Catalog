using LiteBus.Queries.Abstractions;
using FoodManager.Catalog.Application.Mappers;
using FoodManager.Catalog.Domain.Filters;
using FoodManager.Catalog.Application.Output.Response;
using FoodManager.Catalog.Domain.Interfaces.Services;
using FoodManager.Catalog.Domain.Interfaces.Repositories;
using FoodManager.Catalog.Catalog.Domain.Results;

namespace FoodManager.Catalog.Application.Output.Queries
{
    public class GetFoodQueryHandler(
       IFoodRepository _repository,
       ICacheService cacheService) : IQueryHandler<GetFoodQuery, Result<PagedResult<GetFoodResponse>>>
    {
        public async Task<Result<PagedResult<GetFoodResponse>>> HandleAsync(GetFoodQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"Foods_{string.Join(',', request.Foodequest.Ids ?? [])}_" +
               $"{string.Join(',', request.Foodequest.Names ?? [])}_" +
               $"{string.Join(',', request.Foodequest.Assessment ?? [])}_" +
               $"{string.Join(',', request.Foodequest.Categories ?? [])}";

            var cached = await cacheService.GetCacheValueAsync<IEnumerable<GetFoodResponse>>(cacheKey);

            if (cached is null || !cached.Any())
            {
                var foodFilterBuilder = new FoodFiltersBuilder.Builder()
                    .WithAssessment(request.Foodequest.Assessment)
                    .WithCategorys(request.Foodequest.Categories)
                    .WithFoodIds(request.Foodequest.Ids)
                    .WithNames(request.Foodequest.Names)
                    .Build();

                var foods = await _repository.GetFoodsAsync(foodFilterBuilder, cancellationToken);

                var result = foods.ToResponse(request.Foodequest.PageFilter);

                await cacheService.SetCacheValueAsync("Foods", result.ToResponse(), TimeSpan.FromDays(7));

                return Result<PagedResult<GetFoodResponse>>.Success(result);
            }

            return Result<PagedResult<GetFoodResponse>>.Success(cached.ToResponse(request.Foodequest.PageFilter));
        }
    }
}