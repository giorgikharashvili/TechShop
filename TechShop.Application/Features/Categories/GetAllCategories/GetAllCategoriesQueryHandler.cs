using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Categories;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Categories.GetAllCategories;

public class GetAllCategoriesQueryHandler(
    IRepository<Domain.Entities.Categories> _repository,
    IMapper _mapper,
    ILogger<GetAllCategoriesQueryHandler> _logger
    ) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoriesDto>>
{
    public async Task<IEnumerable<CategoriesDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllCategoriesQuery");

        var categories = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} categories from repository", categories.Count());

        return _mapper.Map<IEnumerable<CategoriesDto>>(categories);
    }
}
