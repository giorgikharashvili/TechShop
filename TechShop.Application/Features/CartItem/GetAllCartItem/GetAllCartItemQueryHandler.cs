using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.CartItem;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetAllCartItem
{
    public class GetAllCartQueryHandler : IRequestHandler<GetAllCartItemQuery, IEnumerable<CartItemDto>>
    {
        private readonly IRepository<CartItem> _repository;
        private readonly IMapper _mapper;

        public GetAllCartQueryHandler(IRepository<CartItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CartItemDto>> Handle(GetAllCartItemQuery request, CancellationToken cancellationToken)
        {
            var CartItem = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CartItemDto>>(CartItem);
        }
    }
}
