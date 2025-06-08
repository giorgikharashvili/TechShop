using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkus.UpdateProductsSkus;

public class UpdateProductsSkusCommandHandler(
    IRepository<Domain.Entities.ProductsSkus> _repository,
    IMapper _mapper,
    ILogger<UpdateProductsSkusCommandHandler> _logger
    ) : IRequestHandler<UpdateProductsSkusCommand, bool>
{
    public async Task<bool> Handle(UpdateProductsSkusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateProductsSkusCommand for SKU ID: {Id}", request.id);

        var productsSkus = await _repository.GetByIdAsync(request.id);
        if (productsSkus == null)
        {
            _logger.LogWarning("Product SKU with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request, productsSkus);
        _logger.LogInformation("Mapped update request to Product SKU entity.");

        await _repository.UpdateAsync(productsSkus);
        _logger.LogInformation("Product SKU with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
