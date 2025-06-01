using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Application.Features.Address.GetAllOrderItem;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetAllOrderItem
{
    public class GetAllOrderItemQueryHandler : IRequestHandler<GetAllOrderItemQuery, IEnumerable<OrderItemDto>>
    {
        private readonly IRepository<OrderItem> _repository;
        private readonly IMapper _mapper;

        public GetAllOrderItemQueryHandler(IRepository<OrderItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemDto>> Handle(GetAllOrderItemQuery request, CancellationToken cancellationToken)
        {
            var orderItem = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderItemDto>>(orderItem);
        }
    }
}
