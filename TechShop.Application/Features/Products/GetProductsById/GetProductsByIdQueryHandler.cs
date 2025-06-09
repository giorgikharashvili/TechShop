using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Products;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.GetProductsById;

public class GetProductsByIdQueryHandler(
    IRepository<Domain.Entities.Products> _repository,
    IMapper _mapper,
    ILogger<GetProductsByIdQueryHandler> _logger
    ) : IRequestHandler<GetProductsByIdQuery, ProductsDto>
{
    public async Task<ProductsDto> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetProductsByIdQuery for Product ID: {Id}", request.id);

        var entity = await _repository.GetByIdAsync(request.id);
        if (entity == null)
        {
            _logger.LogWarning("Product with ID: {Id} not found.", request.id);
            return null;
        }

        _logger.LogInformation("Product found. Mapping to ProductsDto.");
        return _mapper.Map<ProductsDto>(entity);
    }
}
