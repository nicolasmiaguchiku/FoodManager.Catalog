using FastMenu.Domain.Interfaces;
using FastMenu.Domain.Results;
using LiteBus.Queries.Abstractions;
using FastMenu.Domain.Dtos.Response;
using FastMenu.Domain.Services;
using FastMenu.Application.Mappers;

namespace FastMenu.Application.Output.Queries
{
    public class GetFoodQueryHandler(
       IFoodRepository foodRepository,
       ICacheService cacheService) : IQueryHandler<GetFoodQuery, Result<IEnumerable<FoodResponse>>>
    {
        public async Task<Result<IEnumerable<FoodResponse>>> HandleAsync(GetFoodQuery request, CancellationToken cancellationToken)
        {
            var cached = await cacheService.GetCacheValueAsync<IEnumerable<FoodResponse>>("Foods");

            if (cached is null || !cached.Any())
            {
                var response = await foodRepository.GetAllAsync(cancellationToken);

                var result = response.Select(x => x.ToResponse());

                await cacheService.SetCacheValueAsync("Foods", result, TimeSpan.FromDays(7));

                return Result<IEnumerable<FoodResponse>>.Success(result);
            }

            return Result<IEnumerable<FoodResponse>>.Success(cached);
        }
    }
}