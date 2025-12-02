using FluentValidation;
using FoodManager.Catalog.Application.Dtos;

namespace FoodManager.Catalog.Application.Validations
{
    public class FoodDtoValidator : AbstractValidator<FoodDto>
    {
        public FoodDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome do alimento é obrigatório.")
                .MinimumLength(3)
                .WithMessage("O nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(100)
                .WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("O preço deve ser maior que zero.");

            RuleFor(x => x.Assessment)
                .InclusiveBetween(1, 5)
                .WithMessage("A avaliação deve estar entre 1 e 5.");

            RuleFor(x => x.Description)
                .MaximumLength(150)
                .WithMessage("A descrição deve ter no máximo 150 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.Category)
                .IsInEnum()
                .WithMessage("A categoria informada é inválida.");
        }
    }
}
