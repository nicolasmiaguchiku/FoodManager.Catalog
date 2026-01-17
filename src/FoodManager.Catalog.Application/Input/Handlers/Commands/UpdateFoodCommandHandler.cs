using FluentValidation;
using FoodManager.Catalog.Application.Mappers;
using FoodManager.Catalog.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Dtos;
using FoodManager.Internal.Shared.Http.Catalog.Errors;
using LiteBus.Commands.Abstractions;
using Mattioli.Configurations.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands
{
    public sealed class UpdateFoodCommandHandler(
        IFoodRepository foodRepository,
        IValidator<FoodDto> foodDtoValidator,
        IValidator<JsonPatchError> patchValidator,
        ILogger<UpdateFoodCommandHandler> _logger) : ICommandHandler<UpdateFoodCommand, Result<bool>>
    {
        public async Task<Result<bool>> HandleAsync(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            var foodResult = await foodRepository.GetOneAsync(x => x.Id == request.UpdateFoodRequest.Id, cancellationToken);

            if (foodResult is null)
            {
                _logger.LogInformation("FoodId {FoodId} Not found", foodResult!.Id);
                return Result<bool>.Failure(FoodErrors.FoodDoesNotExist);
            }

            var food = foodResult.ToFoodDto();

            request
                .UpdateFoodRequest
                .FoodPatchDocument
                .ApplyTo(food, error => patchValidator.ValidateAndThrow(error));

            var validation = await foodDtoValidator.ValidateAsync(food, cancellationToken);

            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToArray();
                return Result<bool>.Failure(FoodErrors.ValueMismatch(string.Join("\n", errors)));
            }

            var modified = await foodRepository.ReplaceAsync(x => x.Id == request.UpdateFoodRequest.Id, food.ToDomain(), cancellationToken);

            if (modified == 0)
            {
                _logger.LogInformation("Error update FoodId {FoodId}", foodResult.Id);
                return Result<bool>.Failure(FoodErrors.FoodDoesNotExist);
            }

            _logger.LogInformation("FoodId {FoodId} updated successfully", foodResult.Id);
            return Result<bool>.Success(true);
        }
    }
}