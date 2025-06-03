using FluentValidation;

namespace TechShop.Application.Features.ProductsSkuAttributes.UpdateProductsSkuAttributes
{
    public class UpdateProductsSkuAttributesCommandValidator : AbstractValidator<UpdateProductsSkuAttributesCommand>
    {
        public UpdateProductsSkuAttributesCommandValidator()
        {
            RuleFor(x => x.Dto.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Dto.Type).NotNull().MaximumLength(50);
            RuleFor(x => x.Dto.Value).NotNull().MaximumLength(50);
        }
    }
}
