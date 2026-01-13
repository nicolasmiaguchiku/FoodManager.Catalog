using FoodManager.Catalog.Application.Mappers;
using Mattioli.Configurations.Models;
using FoodManager.Catalog.Domain.Interfaces.Repositories;
using FoodManager.Catalog.Domain.Interfaces.Services;
using LiteBus.Commands.Abstractions;
using Microsoft.Extensions.Logging;
using FoodManager.Internal.Shared.Http.Catalog.Responses;
using FoodManager.Internal.Shared.Services;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands
{
    public sealed class AddFoodCommandHandler(IFoodRepository _repository, 
        ICacheService _cache, 
        ILogger<AddFoodCommandHandler> _logger,
        ITenantProvider tenantProvider) : ICommandHandler<AddFoodCommand, Result<GetFoodResponse>>
    {
        public async Task<Result<GetFoodResponse>> HandleAsync(AddFoodCommand request, CancellationToken cancellationToken = default)
        {
            var tenant = tenantProvider.GetTenant();

            var result = request.FoodRequest.ToEntity(tenant);

            await _repository.AddAsync(result, cancellationToken);

            await _cache.SetCacheValueAsync("Foods", result.ToResponse(), TimeSpan.FromDays(7));

            _logger.LogInformation("FoodName {FoodName} add successfully", result.Name);

            return Result<GetFoodResponse>.Success(result.ToResponse());
        }
    }
}