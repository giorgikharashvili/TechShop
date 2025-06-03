using FluentValidation;

namespace TechShop.Application.Features.ProductsSkus.CreateProductsSkus
{
    public class CreateProductsSkusCommandValidator : AbstractValidator<CreateProductsSkusCommand>
    {
        public CreateProductsSkusCommandValidator()
        {
            RuleFor(x => x.Dto.StockQuantity).NotEmpty();
            RuleFor(x => x.Dto.Sku).NotEmpty();
            RuleFor(x => x.Dto.Price).NotNull().GreaterThan(0);
            RuleFor(x => x.Dto.ProductId).NotNull().GreaterThan(0);
        }
    }
}
