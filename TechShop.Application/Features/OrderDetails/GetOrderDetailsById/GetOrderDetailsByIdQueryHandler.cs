using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderDetails.GetOrderDetailsById
{
    public class GetOrderDetailsByIdQueryHandler : IRequestHandler<GetOrderDetailsByIdQuery, OrderDetailsDto?>
    {
        private readonly IRepository<Domain.Entities.OrderDetails> _repository;
        private readonly IMapper _mapper;

        public GetOrderDetailsByIdQueryHandler(IRepository<Domain.Entities.OrderDetails> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderDetailsDto?> Handle(GetOrderDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var orderDetails = await _repository.GetByIdAsync(request.id);
            if (orderDetails == null) return null;

            return _mapper.Map<OrderDetailsDto>(orderDetails);
        }
    }
}
