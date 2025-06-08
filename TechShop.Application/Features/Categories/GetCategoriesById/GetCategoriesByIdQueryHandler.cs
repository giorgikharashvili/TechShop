using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Categories;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Categories.GetCategoriesById;

public class GetCategoriesByIdQueryHandler(
    IRepository<Domain.Entities.Categories> _repository,
    IMapper _mapper,
    ILogger<GetCategoriesByIdQueryHandler> _logger
    ) : IRequestHandler<GetCategoriesByIdQuery, CategoriesDto?>
{
    public async Task<CategoriesDto?> Handle(GetCategoriesByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetCategoriesByIdQuery for Category ID: {Id}", request.id);

        var categories = await _repository.GetByIdAsync(request.id);
        if (categories == null)
        {
            _logger.LogWarning("Category with ID: {Id} not found.", request.id);
            return null;
        }

        _logger.LogInformation("Category found. Mapping to CategoriesDto.");
        return _mapper.Map<CategoriesDto>(categories);
    }
}
