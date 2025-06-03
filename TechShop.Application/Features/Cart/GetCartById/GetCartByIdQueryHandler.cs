using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Cart;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Cart.GetCartById
{
    public class GetCartItemByIdQueryHandler : IRequestHandler<GetCartByIdQuery, CartDto?>
    {
        private readonly IRepository<Domain.Entities.Cart> _repository;
        private readonly IMapper _mapper;

        public GetCartItemByIdQueryHandler(IRepository<Domain.Entities.Cart> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CartDto?> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetByIdAsync(request.id);
            if (cart == null) return null;

            return _mapper.Map<CartDto>(cart);
        }
    }
}
