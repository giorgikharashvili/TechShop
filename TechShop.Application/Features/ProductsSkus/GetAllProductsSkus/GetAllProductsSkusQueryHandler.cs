using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.ProductsSkus;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkus.GetAllProductsSkus;

public class GetAllProductsSkusQueryHandler(
    IRepository<Domain.Entities.ProductsSkus> _repository,
    IMapper _mapper,
    ILogger<GetAllProductsSkusQueryHandler> _logger
    ) : IRequestHandler<GetAllProductsSkusQuery, IEnumerable<ProductsSkusDto>>
{
    public async Task<IEnumerable<ProductsSkusDto>> Handle(GetAllProductsSkusQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllProductsSkusQuery");

        var productsSkus = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} product SKUs from repository", productsSkus.Count());

        return _mapper.Map<IEnumerable<ProductsSkusDto>>(productsSkus);
    }
}
