using FoodManager.Domain.Interfaces;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Application.Input.Handlers.Commands
{
    public sealed class DeleteFoodCommandHandler(IFoodRepository repository) : ICommandHandler<DeleteFoodCommand>
    {
        public async Task HandleAsync(DeleteFoodCommand message, CancellationToken cancellationToken = default)
        {
           await repository.DeleteAsync(x => x.Id == message.Id, cancellationToken);
        }
    }
}