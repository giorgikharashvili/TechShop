using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.CreateProductsSkus
{
    public class CreateProductsSkusCommandHandler : IRequestHandler<CreateProductsSkusCommand, int>
    {
        private readonly IRepository<ProductsSkus> _repository;
        private readonly IMapper _mapper;
        
        public CreateProductsSkusCommandHandler(IRepository<ProductsSkus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> Handle(CreateProductsSkusCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ProductsSkus>(request);
            await _repository.AddAsync(entity);
            return entity.Id;
        }
    }
}
