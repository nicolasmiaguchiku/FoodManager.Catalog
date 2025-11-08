using FoodManager.Domain.Enums;

namespace FoodManager.Application.Input.Requests
{
   public sealed record FoodRequest
   (
       string Name,
       decimal Price,
       string Description,
       int Assessment,
       Category Category
   );
}