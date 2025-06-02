using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderItem.CreateOrderItem
{
    public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, OrderItemDto>
    {
        private readonly IRepository<Domain.Entities.OrderItem> _repository;
        private readonly IMapper _mapper;
        
        public CreateOrderItemCommandHandler(IRepository<Domain.Entities.OrderItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<OrderItemDto> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.OrderItem>(request);
            await _repository.AddAsync(entity);
            var dto = _mapper.Map<OrderItemDto>(entity);
            return dto;
        }
    }
}
