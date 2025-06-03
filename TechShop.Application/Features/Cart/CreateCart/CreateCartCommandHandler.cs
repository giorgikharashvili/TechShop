using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Cart;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Cart.CreateCart
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, CartDto>
    {
        private readonly IRepository<Domain.Entities.Cart> _repository;
        private readonly IMapper _mapper;
        
        public CreateCartCommandHandler(IRepository<Domain.Entities.Cart> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<CartDto> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Cart>(request.Dto);
            await _repository.AddAsync(entity);

            var dto = _mapper.Map<CartDto>(entity);

            return dto;
        }
    }
}
