using FoodManager.Catalog.Catalog.Domain.Results;

namespace FoodManager.Catalog.Domain.Errors
{
    public static class FoodErrors
    {
        public static Error FoodDoesNotExist => new("FoodDoesNotExist", "The provided ID does not exist!");

        public static Error ValueMismatch(string description) => new("ValueMismatch", description);
    }
}