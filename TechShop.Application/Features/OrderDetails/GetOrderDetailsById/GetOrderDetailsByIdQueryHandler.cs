using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetOrderDetailsById
{
    public class GetOrderDetailsByIdQueryHandler : IRequestHandler<GetOrderDetailsByIdQuery, OrderDetailsDto?>
    {
        private readonly IRepository<OrderDetails> _repository;
        private readonly IMapper _mapper;

        public GetOrderDetailsByIdQueryHandler(IRepository<OrderDetails> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderDetailsDto?> Handle(GetOrderDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var orderDetails = await _repository.GetByIdAsync(request.id);
            if (orderDetails == null) return null;
            return _mapper.Map<OrderDetailsDto>(orderDetails);
        }
    }
}
