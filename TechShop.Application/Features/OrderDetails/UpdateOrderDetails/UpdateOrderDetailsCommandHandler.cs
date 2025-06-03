using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderDetails.UpdateOrderDetails
{
    public class UpdateOrderDetailsCommandHandler : IRequestHandler<UpdateOrderDetailsCommand, bool>
    {
        private readonly IRepository<Domain.Entities.OrderDetails> _repository;
        private readonly IMapper _mapper;

        public UpdateOrderDetailsCommandHandler(IRepository<Domain.Entities.OrderDetails> repository, IMapper mapper)
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
