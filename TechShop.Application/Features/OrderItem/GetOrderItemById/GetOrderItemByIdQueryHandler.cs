using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetOrderItemById
{
    public class GetOrderItemByIdQueryHandler : IRequestHandler<GetOrderItemByIdQuery, OrderItemDto?>
    {
        private readonly IRepository<Addresses> _repository;
        private readonly IMapper _mapper;

        public GetOrderItemByIdQueryHandler(IRepository<Addresses> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderItemDto?> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
        {
            var orderItem = await _repository.GetByIdAsync(request.id);
            if (orderItem == null) return null;
            return _mapper.Map<OrderItemDto>(orderItem);
        }
    }
}
