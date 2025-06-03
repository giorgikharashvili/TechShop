using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderItem.DeleteOrderItem
{
    public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, bool>
    {
        private readonly IRepository<Domain.Entities.OrderItem> _repository;

        public DeleteOrderItemCommandHandler(IRepository<Domain.Entities.OrderItem> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;

            await _repository.DeleteAsync(request.id);

            return true;
        }
    }
}
