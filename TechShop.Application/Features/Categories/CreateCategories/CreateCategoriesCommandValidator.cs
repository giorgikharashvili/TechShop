using FluentValidation;

namespace TechShop.Application.Features.Categories.CreateCategories
{
    public class CreateCategoriesCommandValidator : AbstractValidator<CreateCategoriesCommand>
    {
        public CreateCategoriesCommandValidator()
        {
            RuleFor(x => x.Dto.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Dto.Description).NotEmpty().MaximumLength(500);
        }
    }
}
