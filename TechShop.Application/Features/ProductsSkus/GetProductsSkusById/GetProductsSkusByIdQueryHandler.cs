using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.ProductsSkus;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetProductsSkusById
{
    public class GetProductsSkusByIdQueryHandler : IRequestHandler<GetProductsSkusByIdQuery, ProductsSkusDto?>
    {
        private readonly IRepository<ProductsSkus> _repository;
        private readonly IMapper _mapper;

        public GetProductsSkusByIdQueryHandler(IRepository<ProductsSkus> repository, IMapper mapper)
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
