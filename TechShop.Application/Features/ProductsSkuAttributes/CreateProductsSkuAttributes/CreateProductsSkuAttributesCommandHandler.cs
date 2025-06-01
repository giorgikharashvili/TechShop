using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Application.Features.Address.CreateProductsSkuAttributes;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.CreateProductsSkuAttributes
{
    public class CreateProductsSkuAttributesCommandHandler : IRequestHandler<CreateProductsSkuAttributesCommand, int>
    {
        private readonly IRepository<ProductSkuAttributes> _repository;
        private readonly IMapper _mapper;
        
        public CreateProductsSkuAttributesCommandHandler(IRepository<ProductSkuAttributes> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> Handle(CreateProductsSkuAttributesCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ProductSkuAttributes>(request);
            await _repository.AddAsync(entity);
            return entity.Id;
        }
    }
}
