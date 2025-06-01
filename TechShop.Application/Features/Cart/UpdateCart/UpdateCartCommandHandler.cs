using AutoMapper;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.UpdateCart
{
    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, bool>
    {
        private readonly IRepository<Cart> _repository;
        private readonly IMapper _mapper;

        public UpdateCartCommandHandler(IRepository<Cart> repository, IMapper mapper)
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
