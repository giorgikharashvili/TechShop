using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.ProductsSkuAttributes;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkuAttributes.GetProductsSkuAttributesById;

public class GetProductsSkuAttributesByIdQueryHandler(
    IRepository<ProductSkuAttributes> _repository,
    IMapper _mapper,
    ILogger<GetProductsSkuAttributesByIdQueryHandler> _logger
    ) : IRequestHandler<GetProductsSkuAttributesByIdQuery, ProductSkuAttributesDto?>
{
    public async Task<ProductSkuAttributesDto?> Handle(GetProductsSkuAttributesByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetProductsSkuAttributesByIdQuery for ID: {Id}", request.id);

        var productSkuAttributes = await _repository.GetByIdAsync(request.id);
        if (productSkuAttributes == null)
        {
            _logger.LogWarning("ProductSkuAttributes with ID: {Id} not found.", request.id);
            return null;
        }

        _logger.LogInformation("ProductSkuAttributes found. Mapping to DTO.");
        return _mapper.Map<ProductSkuAttributesDto>(productSkuAttributes);
    }
}
