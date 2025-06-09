using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.ProductsSkus;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkus.CreateProductsSkus;

public class CreateProductsSkusCommandHandler(
    IRepository<Domain.Entities.ProductsSkus> _repository,
    IMapper _mapper,
    ILogger<CreateProductsSkusCommandHandler> _logger
    ) : IRequestHandler<CreateProductsSkusCommand, ProductsSkusDto>
{
    public async Task<ProductsSkusDto> Handle(CreateProductsSkusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateProductsSkusCommand for SKU: {Sku}", request.Dto.Sku);

        var entity = _mapper.Map<Domain.Entities.ProductsSkus>(request.Dto);
        await _repository.AddAsync(entity);

        _logger.LogInformation("Product SKU created with ID: {Id}", entity.Id);

        var dto = _mapper.Map<ProductsSkusDto>(entity);

        return dto;
    }
}
