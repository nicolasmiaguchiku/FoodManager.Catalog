using FoodManager.Catalog.Domain.Interfaces.Repositories;
using FoodManager.Catalog.Domain.Interfaces.Services;
using FoodManager.Catalog.Domain.ValueObjects;
using FoodManager.Internal.Shared.Http.Catalog.Errors;
using LiteBus.Commands.Abstractions;
using Mattioli.Configurations.Models;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands
{
    public sealed class UploadImageFoodCommandHandler(IFoodRepository foodRepository, IImageStorageService imageStorageService) : ICommandHandler<UploadImageFoodCommand, Result<string>>
    {
        public async Task<Result<string>> HandleAsync(UploadImageFoodCommand message, CancellationToken cancellationToken = default)
        {
            var food = await foodRepository.GetOneAsync(x => x.Id == message.Id, cancellationToken);

            if (food is null)
            {
                return Result<string>.Failure(FoodErrors.FoodDoesNotExist);
            }

            var imagePath = await imageStorageService.UploadAsync(message.Request.File, cancellationToken);

            food.SetImageFile(new FoodImage(imagePath));

            await foodRepository.ReplaceAsync(x => x.Id == food.Id, food, cancellationToken);

            return Result<string>.Success(imagePath);
        }
    }
}