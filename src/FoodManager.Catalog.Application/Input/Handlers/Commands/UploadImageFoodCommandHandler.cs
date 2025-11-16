using LiteBus.Commands.Abstractions;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands
{
    public sealed class UploadImageFoodCommandHandler : ICommandHandler<UploadImageFoodCommand, string>
    {
        public Task<string> HandleAsync(UploadImageFoodCommand message, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}