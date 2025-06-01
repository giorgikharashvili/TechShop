using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.ProductsSkuAttributes;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetProductsSkuAttributesById
{
    public class GetProductsSkuAttributesByIdQueryHandler : IRequestHandler<GetProductsSkuAttributesByIdQuery, ProductSkuAttributesDto?>
    {
        private readonly IRepository<ProductSkuAttributes> _repository;
        private readonly IMapper _mapper;

        public GetProductsSkuAttributesByIdQueryHandler(IRepository<ProductSkuAttributes> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductSkuAttributesDto?> Handle(GetProductsSkuAttributesByIdQuery request, CancellationToken cancellationToken)
        {
            var productSkuAttributes = await _repository.GetByIdAsync(request.id);
            if (productSkuAttributes == null) return null;
            return _mapper.Map<ProductSkuAttributesDto>(productSkuAttributes);
        }
    }
}
