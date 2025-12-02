using FoodManager.Catalog.Domain.Enums;

namespace FoodManager.Catalog.Application.Dtos
{
    public record FoodDto
    {
        public string? Name { get; init; }
        public decimal Price { get; init; }
        public string? Description { get; init; }
        public int Assessment { get; init; }
        public Category Category { get; init; }
    }
}