using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Application.Features.Address.CreateAddresses;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.CreateCart
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, int>
    {
        private readonly IRepository<Cart> _repository;
        private readonly IMapper _mapper;
        
        public CreateCartCommandHandler(IRepository<Cart> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Cart>(request);
            await _repository.AddAsync(entity);
            return entity.Id;
        }
    }
}
