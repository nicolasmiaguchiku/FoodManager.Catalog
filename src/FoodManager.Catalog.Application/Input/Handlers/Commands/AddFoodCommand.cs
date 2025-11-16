using FoodManager.Catalog.Application.Input.Requests;
using FoodManager.Catalog.Application.Output.Response;
using FoodManager.Catalog.Catalog.Domain.Results;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands;

public record AddFoodCommand(AddFoodRequest FoodRequest) : ICommand<Result<GetFoodResponse>>;