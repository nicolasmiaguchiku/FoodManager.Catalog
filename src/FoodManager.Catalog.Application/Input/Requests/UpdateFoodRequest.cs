using FoodManager.Catalog.Application.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace FoodManager.Catalog.Application.Input.Requests
{
    public record UpdateFoodRequest(Guid Id)
    {
        public JsonPatchDocument<FoodDto> FoodPatchDocument { get; set; } = new JsonPatchDocument<FoodDto>();
    }
}