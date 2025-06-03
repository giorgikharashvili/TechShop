using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Cart.UpdateCart
{
    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Cart> _repository;
        private readonly IMapper _mapper;

        public UpdateCartCommandHandler(IRepository<Domain.Entities.Cart> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetByIdAsync(request.id);
            if (cart == null) return false;

            _mapper.Map(request, cart);

            await _repository.UpdateAsync(cart);

            return true;
        }
    }
}
