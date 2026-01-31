using FoodManager.Catalog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace FoodManager.Catalog.Infrastructure.Persistence.Services
{
    public sealed class ImageStorageService : IImageStorageService
    {
        private readonly string _basePath = "C:\\Users\\nicol\\OneDrive\\Imagens\\FoodManager\\foods";
        private readonly string _publicPath = "/images/foods";
        public async Task<string> UploadAsync(IFormFile file, CancellationToken ct = default)
        {
            Directory.CreateDirectory(_basePath);

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(_basePath, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream, ct);

            return $"{_publicPath}/{fileName}";
        }

        public Task DeleteAsync(string filePath, CancellationToken ct = default)
        {
            var physicalPath = Path.Combine(
                _basePath,
                Path.GetFileName(filePath)
            );

            if (File.Exists(physicalPath))
                File.Delete(physicalPath);

            return Task.CompletedTask;
        }
    }
}