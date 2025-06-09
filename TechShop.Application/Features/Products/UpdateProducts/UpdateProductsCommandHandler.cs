using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Products;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.UpdateProducts;

public class UpdateProductsCommandHandler(
    IRepository<Domain.Entities.Products> _repository,
    IMapper _mapper,
    ILogger<UpdateProductsCommandHandler> _logger
    ) : IRequestHandler<UpdateProductsCommand, bool>
{
    public async Task<bool> Handle(UpdateProductsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateProductsCommand for Product ID: {Id}", request.id);

        var entity = await _repository.GetByIdAsync(request.id);
        if (entity == null)
        {
            _logger.LogWarning("Product with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request.Dto, entity);
        _logger.LogInformation("Mapped update request to product entity.");

        entity.ModifiedAt = DateTime.UtcNow;
        entity.ModifiedBy = "System";

        await _repository.UpdateAsync(entity);
        _logger.LogInformation("Product with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
