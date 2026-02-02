using FoodManager.Catalog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace FoodManager.Catalog.Infrastructure.Persistence.Services
{
    public sealed class ImageStorageService : IImageStorageService
    {
        private readonly string _basePath = @"C:\Users\nicol\OneDrive\Imagens\foods";

        public async Task<string> UploadAsync(IFormFile file, CancellationToken ct = default)
        {
            Directory.CreateDirectory(_basePath);

            var fileName = Path.GetFileName(file.FileName);

            var fullPath = Path.Combine(_basePath, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream, ct);

            return fullPath;
        }
    }
}