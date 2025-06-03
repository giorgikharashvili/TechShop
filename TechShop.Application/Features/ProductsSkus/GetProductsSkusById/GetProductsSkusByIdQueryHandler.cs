using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.ProductsSkus;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkus.GetProductsSkusById
{
    public class GetProductsSkusByIdQueryHandler : IRequestHandler<GetProductsSkusByIdQuery, ProductsSkusDto?>
    {
        private readonly IRepository<Domain.Entities.ProductsSkus> _repository;
        private readonly IMapper _mapper;

        public GetProductsSkusByIdQueryHandler(IRepository<Domain.Entities.ProductsSkus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductsSkusDto?> Handle(GetProductsSkusByIdQuery request, CancellationToken cancellationToken)
        {
            var productsSkus = await _repository.GetByIdAsync(request.id);
            if (productsSkus == null) return null;

            return _mapper.Map<ProductsSkusDto>(productsSkus);
        }
    }
}
