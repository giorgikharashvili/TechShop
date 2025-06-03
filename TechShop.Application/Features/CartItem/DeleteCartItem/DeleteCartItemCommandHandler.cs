using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.CartItem.DeleteCartItem
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartItemCommand, bool>
    {
        private readonly IRepository<Domain.Entities.CartItem> _repository;

        public DeleteCartCommandHandler(IRepository<Domain.Entities.CartItem> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;

            await _repository.DeleteAsync(request.id);
            return true;
        }
    }
}
