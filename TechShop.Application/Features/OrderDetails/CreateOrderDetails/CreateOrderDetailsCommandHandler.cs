using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderDetails.CreateOrderDetails
{
    public class CreateOrderDetailsCommandHandler : IRequestHandler<CreateOrderDetailsCommand, OrderDetailsDto>
    {
        private readonly IRepository<Domain.Entities.OrderDetails> _repository;
        private readonly IMapper _mapper;
        
        public CreateOrderDetailsCommandHandler(IRepository<Domain.Entities.OrderDetails> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<OrderDetailsDto> Handle(CreateOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.OrderDetails>(request);
            entity.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(entity);
            var dto = _mapper.Map<OrderDetailsDto>(entity);
            return dto;
        }
    }
}
