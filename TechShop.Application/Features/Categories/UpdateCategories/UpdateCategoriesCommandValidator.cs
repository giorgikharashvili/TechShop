using FluentValidation;
using TechShop.Application.Features.Categories.UpdateCategories;

namespace TechShop.Application.Features.Categories.UpdateCategories
{
    public class UpdateCategoriesCommandValidator : AbstractValidator<UpdateCategoriesCommand>
    {
        public UpdateCategoriesCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
