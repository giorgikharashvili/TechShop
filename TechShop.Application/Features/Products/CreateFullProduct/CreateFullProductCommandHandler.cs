using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Stripe.BillingPortal;
using TechShop.Application.Services.Interfaces;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.CreateFullProduct;

public class CreateFullProductCommandHandler(
    IProductRepository _productRepository,
    IMapper _mapper,
    IStripeService _stripeService,
    ILogger<CreateFullProductCommandHandler> _logger
    ) : IRequestHandler<CreateFullProductCommand, int>
{
    public async Task<int> Handle(CreateFullProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateFullProductCommand for Product: {ProductName}", request.Dto.Name);

        var product = _mapper.Map<Domain.Entities.Products>(request.Dto);
        product.CreatedAt = DateTime.UtcNow;
        product.CreatedBy = "System";

        await _productRepository.AddAsync(product);
        _logger.LogInformation("Product created with ID: {ProductId}", product.Id);

        foreach (var skuDto in request.Dto.Skus)
        {
            var sku = _mapper.Map<Domain.Entities.ProductsSkus>(skuDto);
            sku.ProductId = product.Id;

            await _productRepository.AddSkuAsync(sku);
            _logger.LogInformation("SKU created for ProductId: {ProductId}, SKU: {Sku}", product.Id, sku.Sku);

            (string stripeProductId, string stripePriceId) = await _stripeService.CreateProductWithPriceAsync(
                name: product.Name,
                description: product.Description,
                unitAmount: (long)(sku.Price * 100),
                productId: product.Id,
                currency: "usd",
                lookupKey: sku.Sku
            );

            _logger.LogInformation("Stripe product and price created: StripeProductId={StripeProductId}, StripePriceId={StripePriceId}", stripeProductId, stripePriceId);

            foreach (var attrDto in skuDto.Attributes)
            {
                var attr = _mapper.Map<Domain.Entities.ProductSkuAttributes>(attrDto);
                attr.Id = sku.Id;

                await _productRepository.AddAttributeAsync(attr);
                _logger.LogInformation("Attribute added for SKU ID: {SkuId}", sku.Id);
            }
        }

        _logger.LogInformation("CreateFullProductCommand completed successfully for Product ID: {ProductId}", product.Id);

        return product.Id;
    }
}
