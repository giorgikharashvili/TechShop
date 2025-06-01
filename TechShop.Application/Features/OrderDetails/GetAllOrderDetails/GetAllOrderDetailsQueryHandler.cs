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

namespace TechShop.Application.Features.Address.GetAllOrderDetails
{
    public class GetAllCartQueryHandler : IRequestHandler<GetAllOrderDetailsQuery, IEnumerable<OrderDetailsDto>>
    {
        private readonly IRepository<OrderDetails> _repository;
        private readonly IMapper _mapper;

        public GetAllCartQueryHandler(IRepository<OrderDetails> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDetailsDto>> Handle(GetAllOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var orderDetails = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDetailsDto>>(orderDetails);
        }
    }
}
