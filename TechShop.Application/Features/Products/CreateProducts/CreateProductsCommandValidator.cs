using FluentValidation;

namespace TechShop.Application.Features.Products.CreateProducts
{
    public class CreateProductsCommandValidator : AbstractValidator<CreateProductsCommand>
    {
        public CreateProductsCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(500);
            RuleFor(x => x.CategoryId).GreaterThan(0);
        }
    }
}