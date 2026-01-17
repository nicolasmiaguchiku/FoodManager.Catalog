using FoodManager.Catalog.Application.Mappers;
using FoodManager.Catalog.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Http.Catalog.Responses;
using FoodManager.Internal.Shared.Services;
using LiteBus.Commands.Abstractions;
using Mattioli.Configurations.Models;
using Microsoft.Extensions.Logging;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands
{
    public sealed class AddFoodCommandHandler(
        IFoodRepository _repository,
        ILogger<AddFoodCommandHandler> _logger,
        ITenantProvider tenantProvider) : ICommandHandler<AddFoodCommand, Result<GetFoodResponse>>
    {
        public async Task<Result<GetFoodResponse>> HandleAsync(AddFoodCommand request, CancellationToken cancellationToken = default)
        {
            var tenant = tenantProvider.GetTenant();
            var result = request.FoodRequest.ToEntity(tenant);

            await _repository.AddAsync(result, cancellationToken);

            _logger.LogInformation("FoodName {FoodName} add successfully", result.Name);

            return Result<GetFoodResponse>.Success(result.ToResponse());
        }
    }
}