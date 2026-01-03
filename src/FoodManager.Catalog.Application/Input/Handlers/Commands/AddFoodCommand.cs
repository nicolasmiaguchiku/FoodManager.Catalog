using FoodManager.Internal.Shared.Http.Catalog.Requests;
using FoodManager.Internal.Shared.Http.Catalog.Responses;
using LiteBus.Commands.Abstractions;
using Mattioli.Configurations.Models;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands;

public record AddFoodCommand(AddFoodRequest FoodRequest) : ICommand<Result<GetFoodResponse>>;