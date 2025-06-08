using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Products;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.GetAllProducts;

public class GetAllProductsQueryHandler(
    IRepository<Domain.Entities.Products> _repository,
    IMapper _mapper,
    ILogger<GetAllProductsQueryHandler> _logger
    ) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductsDto>>
{
    public async Task<IEnumerable<ProductsDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllProductsQuery");

        var entities = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} products from repository", entities.Count());

        return _mapper.Map<IEnumerable<ProductsDto>>(entities);
    }
}
