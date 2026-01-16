using FluentValidation;
using FoodManager.Internal.Shared.Http.Catalog.Requests;

namespace FoodManager.Catalog.Application.Validations
{
    public class UpdateFoodRequestValidator : AbstractValidator<UpdateFoodRequest>
    {
        public UpdateFoodRequestValidator() 
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage($"O campo {nameof(UpdateFoodRequest.Id)} é required.");
        }
    }
}