using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Categories.UpdateCategories;

public class UpdateCategoriesCommandHandler(
    IRepository<Domain.Entities.Categories> _repository,
    IMapper _mapper,
    ILogger<UpdateCategoriesCommandHandler> _logger
    ) : IRequestHandler<UpdateCategoriesCommand, bool>
{
    public async Task<bool> Handle(UpdateCategoriesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateCategoriesCommand for Category ID: {Id}", request.id);

        var address = await _repository.GetByIdAsync(request.id);
        if (address == null)
        {
            _logger.LogWarning("Category with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request, address);
        _logger.LogInformation("Mapped update request to existing Category entity.");

        await _repository.UpdateAsync(address);
        _logger.LogInformation("Category with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
