using AutoMapper;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.UpdateCartItem
{
    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, bool>
    {
        private readonly IRepository<CartItem> _repository;
        private readonly IMapper _mapper;

        public UpdateCartCommandHandler(IRepository<CartItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _repository.GetByIdAsync(request.id);
            if (cartItem == null) return false;
            _mapper.Map(request, cartItem);
            await _repository.UpdateAsync(cartItem);
            return true;
        }
    }
}
