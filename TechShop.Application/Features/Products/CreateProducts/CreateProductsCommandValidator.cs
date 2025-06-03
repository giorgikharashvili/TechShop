using FluentValidation;

namespace TechShop.Application.Features.Products.CreateProducts
{
    public class CreateProductsCommandValidator : AbstractValidator<CreateProductsCommand>
    {
        public CreateProductsCommandValidator()
        {
            RuleFor(x => x.Dto.Name).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Dto.Description).NotNull().NotEmpty().MaximumLength(500);
            RuleFor(x => x.Dto.CategoryId).GreaterThan(0);
        }
    }
}