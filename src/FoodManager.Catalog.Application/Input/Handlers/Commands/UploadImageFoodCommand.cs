using FoodManager.Catalog.Application.Input.Requests;
using LiteBus.Commands.Abstractions;
using Mattioli.Configurations.Models;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands;

public sealed record UploadImageFoodCommand(Guid Id, UploadImageFoodRequest Request) : ICommand<Result>;