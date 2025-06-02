using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderItem.GetAllOrderItem
{
    public class GetAllOrderItemQueryHandler : IRequestHandler<GetAllOrderItemQuery, IEnumerable<OrderItemDto>>
    {
        private readonly IRepository<Domain.Entities.OrderItem> _repository;
        private readonly IMapper _mapper;

        public GetAllOrderItemQueryHandler(IRepository<Domain.Entities.OrderItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemDto>> Handle(GetAllOrderItemQuery request, CancellationToken cancellationToken)
        {
            var orderItem = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderItemDto>>(orderItem);
        }
    }
}
