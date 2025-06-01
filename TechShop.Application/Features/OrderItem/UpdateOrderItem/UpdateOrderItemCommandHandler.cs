using AutoMapper;
using MediatR;
using TechShop.Application.Features.Address.UpdateOrderItem;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.UpdateAddresses
{
    public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, bool>
    {
        private readonly IRepository<OrderItem> _repository;
        private readonly IMapper _mapper;

        public UpdateOrderItemCommandHandler(IRepository<OrderItem> repository, IMapper mapper)
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
