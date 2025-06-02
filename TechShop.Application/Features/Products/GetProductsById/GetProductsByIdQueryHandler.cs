using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Products;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.GetProductsById
{
    public class GetProductsByIdQueryHandler : IRequestHandler<GetProductsByIdQuery, ProductsDto>
    {
        private readonly IRepository<Domain.Entities.Products> _repository;
        private readonly IMapper _mapper;

        public GetProductsByIdQueryHandler(IRepository<Domain.Entities.Products> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductsDto> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);
            if (entity == null) return null;
            return _mapper.Map<ProductsDto>(entity);
        }
    }
}