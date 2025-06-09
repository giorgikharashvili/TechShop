using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.ProductsSkus;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkus.GetProductsSkusById;

public class GetProductsSkusByIdQueryHandler(
    IRepository<Domain.Entities.ProductsSkus> _repository,
    IMapper _mapper,
    ILogger<GetProductsSkusByIdQueryHandler> _logger
    ) : IRequestHandler<GetProductsSkusByIdQuery, ProductsSkusDto?>
{
    public async Task<ProductsSkusDto?> Handle(GetProductsSkusByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetProductsSkusByIdQuery for SKU ID: {Id}", request.id);

        var productsSkus = await _repository.GetByIdAsync(request.id);
        if (productsSkus == null)
        {
            _logger.LogWarning("Product SKU with ID: {Id} not found.", request.id);
            return null;
        }

        _logger.LogInformation("Product SKU found. Mapping to DTO.");
        return _mapper.Map<ProductsSkusDto>(productsSkus);
    }
}
