using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.CartItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.CartItem.CreateCartItem
{
    public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, CartItemDto>
    {
        private readonly IRepository<Domain.Entities.CartItem> _repository;
        private readonly IMapper _mapper;
        
        public CreateCartItemCommandHandler(IRepository<Domain.Entities.CartItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<CartItemDto> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.CartItem>(request);
            await _repository.AddAsync(entity);
            var dto = _mapper.Map<CartItemDto>(entity);
            return dto;
        }
    }
}
