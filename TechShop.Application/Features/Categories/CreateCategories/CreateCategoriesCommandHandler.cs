using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Categories;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Categories.CreateCategories;

public class CreateCategoriesCommandHandler(
    IRepository<Domain.Entities.Categories> _repository,
    IMapper _mapper,
    ILogger<CreateCategoriesCommandHandler> _logger
    ) : IRequestHandler<CreateCategoriesCommand, CategoriesDto>
{
    public async Task<CategoriesDto> Handle(CreateCategoriesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateCategoriesCommand");

        var entity = _mapper.Map<Domain.Entities.Categories>(request.Dto);
        await _repository.AddAsync(entity);

        _logger.LogInformation("Category created with ID: {Id}", entity.Id);

        var dto = _mapper.Map<CategoriesDto>(entity);

        return dto;
    }
}
