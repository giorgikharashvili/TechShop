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

namespace TechShop.Application.Features.Address.CreateOrderDetails
{
    public class CreateOrderDetailsCommandHandler : IRequestHandler<CreateOrderDetailsCommand, int>
    {
        private readonly IRepository<OrderDetails> _repository;
        private readonly IMapper _mapper;
        
        public CreateOrderDetailsCommandHandler(IRepository<OrderDetails> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> Handle(CreateOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<OrderDetails>(request);
            entity.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(entity);
            return entity.Id;
        }
    }
}
