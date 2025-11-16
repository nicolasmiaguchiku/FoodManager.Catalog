using LiteBus.Commands.Abstractions;
using Microsoft.AspNetCore.Http;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands;

public sealed record UploadImageFoodCommand(IFormFile File) : ICommand<string>;