using Microsoft.AspNetCore.Http;

namespace FoodManager.Catalog.Domain.Interfaces.Services
{
    public interface IImageStorageService
    {
        Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken);
    }
}