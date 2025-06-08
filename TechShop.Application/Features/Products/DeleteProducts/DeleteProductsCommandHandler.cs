using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.DeleteProducts;

public class DeleteProductsCommandHandler(
    IRepository<Domain.Entities.Products> _repository,
    ILogger<DeleteProductsCommandHandler> _logger
    ) : IRequestHandler<DeleteProductsCommand, bool>
{
    public async Task<bool> Handle(DeleteProductsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteProductsCommand for Product ID: {Id}", request.id);

        var entity = await _repository.GetByIdAsync(request.id);
        if (entity == null)
        {
            _logger.LogWarning("Product with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("Product with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
