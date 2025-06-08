using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Products;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.GetByCategoryId;

public class GetByCategoryIdQueryHandler(
    IProductRepository _productRepository,
    IMapper _mapper,
    ILogger<GetByCategoryIdQueryHandler> _logger
    ) : IRequestHandler<GetByCategoryIdQuery, IEnumerable<ProductsDto>>
{
    public async Task<IEnumerable<ProductsDto>> Handle(GetByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetByCategoryIdQuery for Category ID: {CategoryId}", request.id);

        var products = await _productRepository.GetByCategoryId(request.id);
        if (products == null || !products.Any())
        {
            _logger.LogWarning("No products found for Category ID: {CategoryId}", request.id);
            return [];
        }

        _logger.LogInformation("Found {Count} products for Category ID: {CategoryId}", products.Count(), request.id);
        return _mapper.Map<IEnumerable<ProductsDto>>(products);
    }
}
