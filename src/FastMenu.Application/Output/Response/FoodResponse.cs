using FastMenu.Domain.Enums;

namespace FastMenu.Domain.Dtos.Response
{
    public sealed record FoodResponse
    (
        Guid Id,
        string Name,
        decimal Price,
        string Description,
        int Assessment,
        Category Category
    );
}