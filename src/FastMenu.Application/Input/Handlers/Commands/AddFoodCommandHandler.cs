using FastMenu.Domain.Interfaces;
using LiteBus.Commands.Abstractions;
using FastMenu.Domain.Results;
using FastMenu.Domain.Dtos.Response;
using FastMenu.Domain.Services;
using FastMenu.Application.Mappers;

namespace FastMenu.Application.Input.Handlers.Commands
{
    public  class AddFoodCommandHandler(IFoodRepository foodRepository, ICacheService _cache) : ICommandHandler<AddFoodCommand, Result<FoodResponse>>
    {
        public async Task<Result<FoodResponse>> HandleAsync(AddFoodCommand request, CancellationToken cancellationToken = default)
        {
            var result = request.FoodRequest.ToEntity();

            await foodRepository.AddAsync(result, cancellationToken);

            await _cache.SetCacheValueAsync("Foods", result.ToResponse(), TimeSpan.FromDays(7));

            return Result<FoodResponse>.Success(result.ToResponse());
        }
    }
}