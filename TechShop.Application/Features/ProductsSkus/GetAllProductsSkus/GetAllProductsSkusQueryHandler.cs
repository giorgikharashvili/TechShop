using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.ProductsSkus;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkus.GetAllProductsSkus
{
    public class GetAllProductsSkusQueryHandler : IRequestHandler<GetAllProductsSkusQuery, IEnumerable<ProductsSkusDto>>
    {
        private readonly IRepository<Domain.Entities.ProductsSkus> _repository;
        private readonly IMapper _mapper;

        public GetAllProductsSkusQueryHandler(IRepository<Domain.Entities.ProductsSkus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductsSkusDto>> Handle(GetAllProductsSkusQuery request, CancellationToken cancellationToken)
        {
            var productsSkus = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductsSkusDto>>(productsSkus);
        }
    }
}
