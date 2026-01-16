using FluentValidation;
using FoodManager.Internal.Shared.Dtos;

namespace FoodManager.Catalog.Application.Validations
{
    public class FoodDtoValidator : AbstractValidator<FoodDto>
    {
        public FoodDtoValidator()
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

            RuleFor(x => x.Assessment)
                .InclusiveBetween(1, 5)
                .WithMessage("The rating should be between 1 and 5.");

            RuleFor(x => x.Description)
                .MaximumLength(150)
                .WithMessage("The description should be a maximum of 150 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.Category)
                .IsInEnum()
                .WithMessage("The category entered is invalid.");
        }
    }
}
