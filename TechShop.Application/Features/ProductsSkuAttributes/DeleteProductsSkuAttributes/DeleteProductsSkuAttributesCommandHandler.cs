using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkuAttributes.DeleteProductsSkuAttributes;

public class DeleteProductsSkuAttributesCommandHandler(
    IRepository<ProductSkuAttributes> _repository,
    ILogger<DeleteProductsSkuAttributesCommandHandler> _logger
    ) : IRequestHandler<DeleteProductsSkuAttributesCommand, bool>
{
    public async Task<bool> Handle(DeleteProductsSkuAttributesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteProductsSkuAttributesCommand for Attribute ID: {Id}", request.id);

        var exists = await _repository.GetByIdAsync(request.id);
        if (exists == null)
        {
            _logger.LogWarning("ProductSkuAttribute with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("ProductSkuAttribute with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
