using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Categories.DeleteCategories;

public class DeleteCartCommandHandler(
    IRepository<Domain.Entities.Categories> _repository,
    ILogger<DeleteCartCommandHandler> _logger
    ) : IRequestHandler<DeleteCategoriesCommand, bool>
{
    public async Task<bool> Handle(DeleteCategoriesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteCategoriesCommand for Category ID: {Id}", request.id);

        var exists = await _repository.GetByIdAsync(request.id);
        if (exists == null)
        {
            _logger.LogWarning("Category with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("Category with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
