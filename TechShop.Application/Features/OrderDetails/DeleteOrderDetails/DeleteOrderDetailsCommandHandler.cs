using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderDetails.DeleteOrderDetails
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteOrderDetailsCommand, bool>
    {
        private readonly IRepository<Domain.Entities.OrderDetails> _repository;

        public DeleteCartCommandHandler(IRepository<Domain.Entities.OrderDetails> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;
            await _repository.DeleteAsync(request.id);
            return true;
        }
    }
}
