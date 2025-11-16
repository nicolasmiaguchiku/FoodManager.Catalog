using FoodManager.Catalog.Domain.Enums;

namespace FoodManager.Catalog.Application.Output.Response
{
    public sealed record GetFoodResponse
    (
        Guid Id,
        string Name,
        decimal Price,
        string Description,
        int Assessment,
        Category Category
    );
}