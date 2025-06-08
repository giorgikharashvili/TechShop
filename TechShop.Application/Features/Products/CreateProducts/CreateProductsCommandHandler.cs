using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Products;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.CreateProducts;

public class CreateProductsCommandHandler(IRepository<Domain.Entities.Products> _repository, IMapper _mapper) : IRequestHandler<CreateProductsCommand, ProductsDto>
{
    public async Task<ProductsDto> Handle(CreateProductsCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Products>(request.Dto);
        entity.CreatedAt = DateTime.UtcNow;

        await _repository.AddAsync(entity);

        var dto = _mapper.Map<ProductsDto>(entity);

        return dto;
    }
}

