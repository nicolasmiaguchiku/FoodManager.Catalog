using FoodManager.Catalog.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Http.Catalog.Errors;
using LiteBus.Commands.Abstractions;
using Mattioli.Configurations.Models;
using Microsoft.Extensions.Logging;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands
{
    public sealed class DeleteFoodCommandHandler(IFoodRepository _repository, ILogger<DeleteFoodCommandHandler> _logger) : ICommandHandler<DeleteFoodCommand, Result<bool>>
    {
        public async Task<Result<bool>> HandleAsync(DeleteFoodCommand message, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("FoodId {FoodName} deleted successfully", message.Id);
            var result = await _repository.DeleteAsync(x => x.Id == message.Id, cancellationToken);

            if (result == 0)
            {
                return Result<bool>.Failure(FoodErrors.FoodDoesNotExist);
            }

            return Result<bool>.Success(true);
        }
    }
}