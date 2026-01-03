using LiteBus.Commands.Abstractions;
using Mattioli.Configurations.Models;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands;

public sealed record DeleteFoodCommand(Guid Id) : ICommand<Result<bool>>;