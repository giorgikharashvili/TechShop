using FluentValidation;

namespace TechShop.Application.Features.ProductsSkus.CreateProductsSkus
{
    public class CreateProductsSkusCommandValidator : AbstractValidator<CreateProductsSkusCommand>
    {
        public CreateProductsSkusCommandValidator()
        {
            RuleFor(x => x.StockQuantity).NotEmpty();
            RuleFor(x => x.Sku).NotEmpty();
            RuleFor(x => x.Price).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductId).NotNull().GreaterThan(0);
        }
    }
}
