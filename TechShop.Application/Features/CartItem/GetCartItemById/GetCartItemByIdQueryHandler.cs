using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.CartItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.CartItem.GetCartItemById
{
    public class GetCartItemByIdQueryHandler : IRequestHandler<GetCartByIdQuery, CartItemDto?>
    {
        private readonly IRepository<Domain.Entities.CartItem> _repository;
        private readonly IMapper _mapper;

        public GetCartItemByIdQueryHandler(IRepository<Domain.Entities.CartItem> repository, IMapper mapper)
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
