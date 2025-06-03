using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Products;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductsDto>>
    {
        private readonly IRepository<Domain.Entities.Products> _repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IRepository<Domain.Entities.Products> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductsDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductsDto>>(entities);
        }
    }
}