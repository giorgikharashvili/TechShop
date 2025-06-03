using FluentValidation;
using TechShop.Domain.DTOs.ProductsSkuAttributes;

namespace TechShop.Application.Features.ProductsSkuAttributes.CreateProductsSkuAttributes
{
    public class CreateProductsSkuAttributesCommandValidator :  AbstractValidator<CreateProductSkuAttributesDto>
    {
        public CreateProductsSkuAttributesCommandValidator()
        {
            RuleFor(x => x.Value).NotNull().MaximumLength(50);
            RuleFor(x => x.Type).NotNull().MaximumLength(50);
        }
    }
}
