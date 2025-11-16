using FoodManager.Catalog.Domain.Interfaces.Repositories;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Catalog.Application.Input.Handlers.Commands
{
    public sealed class DeleteFoodCommandHandler(IFoodRepository _repository) : ICommandHandler<DeleteFoodCommand>
    {
        public async Task HandleAsync(DeleteFoodCommand message, CancellationToken cancellationToken = default)
        {
           await _repository.DeleteAsync(x => x.Id == message.Id, cancellationToken);
        }
    }
}