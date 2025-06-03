using FluentValidation;

namespace TechShop.Application.Features.ProductsSkus.UpdateProductsSkus
{
    public class UpdateProductsSkusCommandValidator : AbstractValidator<UpdateProductsSkusCommand>
    {
        public UpdateProductsSkusCommandValidator()
        {
            RuleFor(x => x.Dto.Price).NotNull().GreaterThan(0);
            RuleFor(x => x.Dto.Sku).NotNull().NotEmpty();
            RuleFor(x => x.Dto.StockQuantity).NotNull().NotEmpty();
            RuleFor(x => x.Dto.ProductId).NotNull().GreaterThan(0);
        }
    }
}
