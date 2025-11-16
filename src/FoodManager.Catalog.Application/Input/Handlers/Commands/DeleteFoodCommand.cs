using LiteBus.Commands.Abstractions;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands;

public sealed record DeleteFoodCommand(Guid Id) : ICommand;