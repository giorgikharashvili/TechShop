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

namespace TechShop.Application.Features.Address.GetCartItemById
{
    public class GetCartItemByIdQueryHandler : IRequestHandler<GetCartByIdQuery, CartItemDto?>
    {
        private readonly IRepository<CartItem> _repository;
        private readonly IMapper _mapper;

        public GetCartItemByIdQueryHandler(IRepository<CartItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CartItemDto?> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var address = await _repository.GetByIdAsync(request.id);
            if (address == null) return null;
            return _mapper.Map<CartItemDto>(address);
        }
    }
}
