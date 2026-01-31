namespace FoodManager.Catalog.CrossCutting.Models
{
    public sealed class ImageStorageSettings
    {
        public string BasePath { get; set; } = default!;
        public string PublicPath { get; set; } = default!;
    }
}