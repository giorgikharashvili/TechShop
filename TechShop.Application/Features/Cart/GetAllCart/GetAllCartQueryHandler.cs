using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Cart;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Address.GetAllCart
{
    public class GetAllCartQueryHandler : IRequestHandler<GetAllCartQuery, IEnumerable<CartDto>>
    {
        private readonly IRepository<Cart> _repository;
        private readonly IMapper _mapper;

        public GetAllCartQueryHandler(IRepository<Cart> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }   

        public async Task<IEnumerable<CartDto>> Handle(GetAllCartQuery request, CancellationToken cancellationToken)
        {
            var Cart = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CartDto>>(Cart);
        }
    }
}
