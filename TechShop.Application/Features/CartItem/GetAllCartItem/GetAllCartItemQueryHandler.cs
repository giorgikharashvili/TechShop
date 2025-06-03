using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.CartItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.CartItem.GetAllCartItem
{
    public class GetAllCartQueryHandler : IRequestHandler<GetAllCartItemQuery, IEnumerable<CartItemDto>>
    {
        private readonly IRepository<Domain.Entities.CartItem> _repository;
        private readonly IMapper _mapper;

        public GetAllCartQueryHandler(IRepository<Domain.Entities.CartItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CartItemDto>> Handle(GetAllCartItemQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<CartItemDto>>(entity);
        }
    }
}
