using FastMenu.Domain.Dtos.Requests;
using FastMenu.Domain.Dtos.Response;
using FastMenu.Domain.Entities;

namespace FastMenu.Application.Mappers
{
    public static class FoodMapper
    {
        public static FoodEntity ToEntity(this FoodRequest food)
        {
            return new FoodEntity
            {
                Id = Guid.NewGuid(),
                Name = food.Name,
                Price = food.Price,
                Description = food.Description,
                Assessment = food.Assessment,
                Category = food.Category,
            };
        }

        public static FoodResponse ToResponse(this FoodEntity entity)
        {
            var food = new FoodResponse(
                entity.Id,
                entity.Name ?? "",
                entity.Price,
                entity.Description ?? "",
                entity.Assessment,
                entity.Category);

            return food;
        }
    }
}