using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Products;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.CreateProducts
{
    public class CreateProductsCommandHandler : IRequestHandler<CreateProductsCommand, ProductsDto>
    {
        private readonly IRepository<Domain.Entities.Products> _repository;
        private readonly IMapper _mapper;

        public CreateProductsCommandHandler(IRepository<Domain.Entities.Products> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductsDto> Handle(CreateProductsCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Products>(request.Dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(entity);

            var dto = _mapper.Map<ProductsDto>(entity);

            return dto;
        }
    }
}
