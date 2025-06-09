using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.ProductsSkuAttributes;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkuAttributes.CreateProductsSkuAttributes;

public class CreateProductsSkuAttributesCommandHandler(
    IRepository<ProductSkuAttributes> _repository,
    IMapper _mapper,
    ILogger<CreateProductsSkuAttributesCommandHandler> _logger
    ) : IRequestHandler<CreateProductsSkuAttributesCommand, ProductSkuAttributesDto>
{
    public async Task<ProductSkuAttributesDto> Handle(CreateProductsSkuAttributesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateProductsSkuAttributesCommand for SKU");

        var entity = _mapper.Map<ProductSkuAttributes>(request.Dto);
        await _repository.AddAsync(entity);

        _logger.LogInformation("ProductSkuAttribute created with ID: {Id}", entity.Id);

        var dto = _mapper.Map<ProductSkuAttributesDto>(entity);

        return dto;
    }
}
