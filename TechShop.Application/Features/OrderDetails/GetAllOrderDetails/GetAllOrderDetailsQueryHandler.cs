using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.Infrastructure.Repositories.Interfaces;


namespace TechShop.Application.Features.OrderDetails.GetAllOrderDetails
{
    public class GetAllCartQueryHandler : IRequestHandler<GetAllOrderDetailsQuery, IEnumerable<OrderDetailsDto>>
    {
        private readonly IRepository<Domain.Entities.OrderDetails> _repository;
        private readonly IMapper _mapper;

        public GetAllCartQueryHandler(IRepository<Domain.Entities.OrderDetails> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderDetailsDto>> Handle(GetAllOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var orderDetails = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDetailsDto>>(orderDetails);
        }
    }
}
