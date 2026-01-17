using FoodManager.Catalog.Application.Mappers;
using FoodManager.Catalog.Domain.Filters;
using FoodManager.Catalog.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Http.Catalog.Responses;
using LiteBus.Queries.Abstractions;
using Mattioli.Configurations.Http;
using Mattioli.Configurations.Models;
using Microsoft.Extensions.Logging;

namespace FoodManager.Catalog.Application.Output.Queries
{
    public class GetFoodQueryHandler(
       IFoodRepository _repository,
       ILogger<GetFoodQueryHandler> _logger) : IQueryHandler<GetFoodQuery, Result<PagedResult<GetFoodResponse>>>
    {
        public async Task<Result<PagedResult<GetFoodResponse>>> HandleAsync(GetFoodQuery request, CancellationToken cancellationToken)
        {
            var foodFilterBuilder = new FoodFiltersBuilder.Builder()
                .WithAssessment(request.Foodequest.Assessment)
                .WithCategorys(request.Foodequest.Categories)
                .WithFoodIds(request.Foodequest.Ids)
                .WithNames(request.Foodequest.Names)
                .Build();

            var foods = await _repository.GetFoodsAsync(foodFilterBuilder, cancellationToken);

            var result = foods.ToResponse(request.Foodequest.PageFilter);

            _logger.LogInformation("Foods retrieved from dataBase successfully");

            return Result<PagedResult<GetFoodResponse>>.Success(result);
        }
    }
}