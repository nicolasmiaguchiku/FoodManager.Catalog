using FluentValidation;
using FoodManager.Catalog.Application.Input.Requests;

namespace FoodManager.Catalog.Application.Validations
{
    public class UpdateFoodRequestValidator : AbstractValidator<UpdateFoodRequest>
    {
        public UpdateFoodRequestValidator() 
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage($"O campo {nameof(UpdateFoodRequest.Id)} é obrigatório.");
        }
    }
}