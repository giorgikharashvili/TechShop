using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.ProductsSkuAttributes;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkuAttributes.GetAllProductsSkuAttributes;

public class GetAllProductsSkuAttributesQueryHandler(
    IRepository<ProductSkuAttributes> _repository,
    IMapper _mapper,
    ILogger<GetAllProductsSkuAttributesQueryHandler> _logger
    ) : IRequestHandler<GetAllProductsSkuAttributesQuery, IEnumerable<ProductSkuAttributesDto>>
{
    public async Task<IEnumerable<ProductSkuAttributesDto>> Handle(GetAllProductsSkuAttributesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllProductsSkuAttributesQuery");

        var productSkuAttributes = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} ProductSkuAttributes from repository", productSkuAttributes.Count());

        return _mapper.Map<IEnumerable<ProductSkuAttributesDto>>(productSkuAttributes);
    }
}
