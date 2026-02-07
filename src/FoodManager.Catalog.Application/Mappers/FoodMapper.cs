using FoodManager.Catalog.Domain.Entities;
using FoodManager.Internal.Shared.Dtos;
using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Http.Catalog.Requests;
using FoodManager.Internal.Shared.Http.Catalog.Responses;
using Mattioli.Configurations.Http;

namespace FoodManager.Catalog.Application.Mappers
{
    public static class FoodMapper
    {
        public static FoodEntity ToEntity(this AddFoodRequest food, Tenant tenant)
        {
            return new FoodEntity.Builder()
                .SetId(Guid.NewGuid())
                .SetName(food.Name)
                .SetPrice(food.Price)
                .SetDescription(food.Description)
                .SetTenant(tenant.Name)
                .SetAssessment(food.Assessment)
                .SetCategory(food.Category)
                .Build();
        }

        public static GetFoodResponse ToResponse(this FoodEntity entity)
        {
            var food = new GetFoodResponse(
                entity.Id,
                entity.Name ?? "",
                entity.Price,
                entity.Description ?? "",
                entity.Assessment,
                entity.Category,
                entity.FoodImage?.Path ?? "");

            return food;
        }

        public static PagedResult<GetFoodResponse> ToResponse(
          this PagedResult<FoodEntity> pagedResult,
          PageFilterRequest pageFilterRequest)
        {
            return new PagedResult<GetFoodResponse>
            {
                PageNumber = pageFilterRequest.Page,
                PageSize = pageFilterRequest.PageSize,
                TotalPages = pagedResult.TotalPages,
                TotalResults = pagedResult.TotalResults,
                Results = pagedResult.Results.Select(result => result.ToResponse())
            };
        }

        public static PagedResult<GetFoodResponse> ToResponse(this IEnumerable<GetFoodResponse> foods, PageFilterRequest pageFilter)
        {
            return new PagedResult<GetFoodResponse>
            {
                PageNumber = pageFilter.Page,
                PageSize = pageFilter.PageSize,
                Results = foods.Select(x => x),
                TotalPages = pageFilter.Page,
                TotalResults = foods.Count()
            };
        }

        public static FoodDto ToFoodDto(this FoodEntity entity)
        {
            return new FoodDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Tenant = entity.Tenant,
                Description = entity.Description,
                Assessment = entity.Assessment,
                Category = entity.Category,
            };
        }

        public static FoodEntity ToDomain(this FoodDto food)
        {
            return new FoodEntity
            {
                Id = food.Id,
                Name = food.Name,
                Price = food.Price,
                Description = food.Description,
                Tenant = food.Tenant!,
                Assessment = food.Assessment,
                Category = food.Category,
            };
        }
    }
}