using FluentValidation;
using FoodManager.Internal.Shared.Http.Catalog.Requests;

namespace FoodManager.Catalog.Application.Validations
{
    public class AddFoodValidator : AbstractValidator<AddFoodRequest>
    {
        public AddFoodValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("The name of the food cannot be null.")
                .NotEmpty()
                .WithMessage("The name of the food is required.");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("The value of food cannot be zero.")
                .NotEmpty()
                .WithMessage("The cost of the food is required.")
                .GreaterThan(0)
                .WithMessage("The value of the food must be greater than 0.");

            RuleFor(x => x.Category)
                .NotNull()
                .WithMessage("The food must have a valid category.")
                .NotEmpty()
                .WithMessage("The food must have a category.");
        }
    }
}