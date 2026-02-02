using Microsoft.AspNetCore.Http;

namespace FoodManager.Catalog.Application.Input.Requests;

public record UploadImageFoodRequest(IFormFile File);