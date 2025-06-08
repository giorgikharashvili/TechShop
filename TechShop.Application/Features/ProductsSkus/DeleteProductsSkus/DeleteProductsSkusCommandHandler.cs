using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkus.DeleteProductsSkus;

public class DeleteProductsSkusCommandHandler(
    IRepository<Domain.Entities.ProductsSkus> _repository,
    ILogger<DeleteProductsSkusCommandHandler> _logger
    ) : IRequestHandler<DeleteProductsSkusCommand, bool>
{
    public async Task<bool> Handle(DeleteProductsSkusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteProductsSkusCommand for SKU ID: {Id}", request.id);

        var exists = await _repository.GetByIdAsync(request.id);
        if (exists == null)
        {
            _logger.LogWarning("Product SKU with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("Product SKU with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
