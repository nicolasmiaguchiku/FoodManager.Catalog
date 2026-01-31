using FoodManager.Catalog.Domain.Enums;
using FoodManager.Catalog.Domain.Interfaces.Repositories;
using FoodManager.Catalog.Domain.Interfaces.Services;
using FoodManager.Catalog.Domain.ValueObjects;
using FoodManager.Internal.Shared.Http.Catalog.Errors;
using LiteBus.Commands.Abstractions;
using Mattioli.Configurations.Models;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands
{
    public sealed class UploadImageFoodCommandHandler(IFoodRepository foodRepository, IImageStorageService imageStorageService) : ICommandHandler<UploadImageFoodCommand, Result>
    {
        public async Task<Result> HandleAsync(UploadImageFoodCommand message, CancellationToken cancellationToken = default)
        {
            var food = await foodRepository.GetOneAsync(x => x.Id == message.Id, cancellationToken);

            if (food is null)
            {
                return Result.Failure(FoodErrors.FoodDoesNotExist);
            }

            var imagePath = await imageStorageService.UploadAsync(message.Request.File, cancellationToken);

            var imageFile = ImageFile.Builder.Create()
                .SetId(Guid.NewGuid())
                .SetDateTime(DateTime.UtcNow)
                .SetFileStatus(FileStatus.Concluded)
                .SetFilePath(imagePath)
                .Build();

            food.SetImageFile(imageFile);

            await foodRepository.ReplaceAsync(x => x.Id == food.Id, food, cancellationToken);
            return Result.Success();
        }
    }
}