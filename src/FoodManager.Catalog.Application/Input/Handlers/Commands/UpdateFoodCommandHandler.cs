using FluentValidation;
using FoodManager.Catalog.Application.Dtos;
using FoodManager.Catalog.Application.Mappers;
using FoodManager.Catalog.Catalog.Domain.Results;
using FoodManager.Catalog.Domain.Errors;
using FoodManager.Catalog.Domain.Interfaces.Repositories;
using LiteBus.Commands.Abstractions;
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
                return Result<bool>.Failure(FoodErrors.FoodDoesNotExist);
            }

            var dto = foodResult.ToFoodDto();

            request
                .UpdateFoodRequest
                .FoodPatchDocument
                .ApplyTo(dto, error => patchValidator.ValidateAndThrow(error));

            foodResult.Name = dto.Name;
            foodResult.Price = dto.Price;
            foodResult.Assessment = dto.Assessment;
            foodResult.Description = dto.Description;
            foodResult.Category = dto.Category;

            var validation = await foodDtoValidator.ValidateAsync(dto, cancellationToken);

            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToArray();
                return Result<bool>.Failure(FoodErrors.ValueMismatch(string.Join("\n", errors)));
            }

            var modified = await foodRepository.ReplaceAsync(x => x.Id == request.UpdateFoodRequest.Id, foodResult, cancellationToken);

            if (modified == 0)
            {
                _logger.LogInformation("FoodId {FoodId} Not found", foodResult.Id);
                return Result<bool>.Failure(FoodErrors.FoodDoesNotExist);
            }

            _logger.LogInformation("FoodId {FoodId} updated successfully", foodResult.Id);
            return Result<bool>.Success(true);
        }
    }
}