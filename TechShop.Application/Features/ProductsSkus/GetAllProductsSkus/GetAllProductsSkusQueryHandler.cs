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

namespace TechShop.Application.Features.Address.GetAllProductsSkus
{
    public class GetAllProductsSkusQueryHandler : IRequestHandler<GetAllProductsSkusQuery, IEnumerable<ProductsSkusDto>>
    {
        private readonly IRepository<ProductsSkus> _repository;
        private readonly IMapper _mapper;

        public GetAllProductsSkusQueryHandler(IRepository<ProductsSkus> repository, IMapper mapper)
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
