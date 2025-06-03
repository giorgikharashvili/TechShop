using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.CartItem.UpdateCartItem
{
    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartItemCommand, bool>
    {
        private readonly IRepository<Domain.Entities.CartItem> _repository;
        private readonly IMapper _mapper;

        public UpdateCartCommandHandler(IRepository<Domain.Entities.CartItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _repository.GetByIdAsync(request.id);
            if (cartItem == null) return false;

            _mapper.Map(request, cartItem);

            await _repository.UpdateAsync(cartItem);
            return true;
        }
    }
}
