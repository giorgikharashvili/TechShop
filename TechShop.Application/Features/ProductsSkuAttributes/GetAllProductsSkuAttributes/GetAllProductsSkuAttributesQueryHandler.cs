using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.ProductsSkuAttributes;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkuAttributes.GetAllProductsSkuAttributes
{
    public class GetAllProductsSkuAttributesQueryHandler : IRequestHandler<GetAllProductsSkuAttributesQuery, IEnumerable<ProductSkuAttributesDto>>
    {
        private readonly IRepository<ProductSkuAttributes> _repository;
        private readonly IMapper _mapper;

        public GetAllProductsSkuAttributesQueryHandler(IRepository<ProductSkuAttributes> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductSkuAttributesDto>> Handle(GetAllProductsSkuAttributesQuery request, CancellationToken cancellationToken)
        {
            var productSkuAttributes = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductSkuAttributesDto>>(productSkuAttributes);
        }
    }
}
