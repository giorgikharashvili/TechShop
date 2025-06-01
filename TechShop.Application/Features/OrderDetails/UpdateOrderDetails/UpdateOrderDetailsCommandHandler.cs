using AutoMapper;
using MediatR;
using TechShop.Application.Features.Address.UpdateOrderDetails;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.UpdateAddresses
{
    public class UpdateOrderDetailsCommandHandler : IRequestHandler<UpdateOrderDetailsCommand, bool>
    {
        private readonly IRepository<OrderDetails> _repository;
        private readonly IMapper _mapper;

        public UpdateOrderDetailsCommandHandler(IRepository<OrderDetails> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            var orderDetails = await _repository.GetByIdAsync(request.id);
            if (orderDetails == null) return false;

            _mapper.Map(request, orderDetails);
            orderDetails.ModifiedAt = DateTime.UtcNow;
            orderDetails.ModifiedBy = "System";

            await _repository.UpdateAsync(orderDetails);
            return true;
        }
    }
}
