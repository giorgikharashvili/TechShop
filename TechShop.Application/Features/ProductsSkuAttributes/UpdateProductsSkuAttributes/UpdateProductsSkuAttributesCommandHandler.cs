using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkuAttributes.UpdateProductsSkuAttributes;

public class UpdateProductsSkuAttributesCommandHandler(
    IRepository<ProductSkuAttributes> _repository,
    IMapper _mapper,
    ILogger<UpdateProductsSkuAttributesCommandHandler> _logger
    ) : IRequestHandler<UpdateProductsSkuAttributesCommand, bool>
{
    public async Task<bool> Handle(UpdateProductsSkuAttributesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateProductsSkuAttributesCommand for ID: {Id}", request.id);

        var productSkuAttributes = await _repository.GetByIdAsync(request.id);
        if (productSkuAttributes == null)
        {
            _logger.LogWarning("ProductSkuAttributes with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request.Dto, productSkuAttributes);
        _logger.LogInformation("Mapped update request to entity.");

        await _repository.UpdateAsync(productSkuAttributes);
        _logger.LogInformation("ProductSkuAttributes with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
