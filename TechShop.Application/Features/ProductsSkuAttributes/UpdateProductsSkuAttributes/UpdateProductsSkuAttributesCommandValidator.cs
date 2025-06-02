using FluentValidation;

namespace TechShop.Application.Features.ProductsSkuAttributes.UpdateProductsSkuAttributes
{
    public class UpdateProductsSkuAttributesCommandValidator : AbstractValidator<UpdateProductsSkuAttributesCommand>
    {
        public UpdateProductsSkuAttributesCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Type).NotNull().MaximumLength(50);
            RuleFor(x => x.Name).NotNull().MaximumLength(50);
        }
    }
}
