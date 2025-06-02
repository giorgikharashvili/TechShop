using FluentValidation;

namespace TechShop.Application.Features.ProductsSkus.UpdateProductsSkus
{
    public class UpdateProductsSkusCommandValidator : AbstractValidator<UpdateProductsSkusCommand>
    {
        public UpdateProductsSkusCommandValidator()
        {
            RuleFor(x => x.Price).NotNull().GreaterThan(0);
            RuleFor(x => x.Sku).NotNull().NotEmpty();
            RuleFor(x => x.StockQuantity).NotNull().NotEmpty();
            RuleFor(x => x.ProductId).NotNull().GreaterThan(0);
        }
    }
}
