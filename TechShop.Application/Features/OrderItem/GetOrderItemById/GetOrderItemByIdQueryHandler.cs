using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderItem.GetOrderItemById
{
    public class GetOrderItemByIdQueryHandler : IRequestHandler<GetOrderItemByIdQuery, OrderItemDto?>
    {
        private readonly IRepository<Domain.Entities.OrderItem> _repository;
        private readonly IMapper _mapper;

        public GetOrderItemByIdQueryHandler(IRepository<Domain.Entities.OrderItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderItemDto?> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
        {
            var orderItem = await _repository.GetByIdAsync(request.id);
            if (orderItem == null) return null;

            return _mapper.Map<OrderItemDto>(orderItem);
        }
    }
}
