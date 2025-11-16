using FoodManager.Catalog.Catalog.Domain.Results;

namespace FoodManager.Catalog.Domain.Errors
{
    public static class FoodErrors
    {
        public static Error ValidationError(string details) => new(
        "CustomerError.ValidationError",
        details);
    }
}
