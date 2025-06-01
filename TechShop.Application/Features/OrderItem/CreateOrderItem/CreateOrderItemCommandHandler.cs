using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Application.Features.Address.CreateOrderItem;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.CreateOrderItem
{
    public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand int>
    {
        private readonly IRepository<OrderItem> _repository;
        private readonly IMapper _mapper;
        
        public CreateOrderItemCommandHandler(IRepository<OrderItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<OrderItem>(request);
            await _repository.AddAsync(entity);
            return entity.Id;
        }
    }
}
