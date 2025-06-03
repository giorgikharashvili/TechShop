using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Cart;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Cart.GetAllCart
{
    public class GetAllCartQueryHandler : IRequestHandler<GetAllCartQuery, IEnumerable<CartDto>>
    {
        private readonly IRepository<Domain.Entities.Cart> _repository;
        private readonly IMapper _mapper;

        public GetAllCartQueryHandler(IRepository<Domain.Entities.Cart> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }   

        public async Task<IEnumerable<CartDto>> Handle(GetAllCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<CartDto>>(cart);
        }
    }
}
