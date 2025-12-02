using FoodManager.Catalog.Application.Input.Requests;
using FoodManager.Catalog.Catalog.Domain.Results;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands;

public sealed record UpdateFoodCommand(UpdateFoodRequest UpdateFoodRequest) : ICommand<Result<bool>>;