using FoodManager.Catalog.Domain.Enums;

namespace FoodManager.Catalog.Application.Input.Requests
{
   public sealed record AddFoodRequest
   (
       string Name,
       decimal Price,
       string Description,
       int Assessment,
       Category Category
   );
}