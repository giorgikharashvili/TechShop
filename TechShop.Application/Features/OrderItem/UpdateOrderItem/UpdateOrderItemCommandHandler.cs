using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderItem.UpdateOrderItem
{
    public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, bool>
    {
        private readonly IRepository<Domain.Entities.OrderItem> _repository;
        private readonly IMapper _mapper;

        public UpdateOrderItemCommandHandler(IRepository<Domain.Entities.OrderItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItem = await _repository.GetByIdAsync(request.id);
            if (orderItem == null) return false;

            _mapper.Map(request, orderItem);

            await _repository.UpdateAsync(orderItem);

            return true;
        }
    }
}
