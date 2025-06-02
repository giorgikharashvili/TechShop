using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Cart.DeleteCart
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Cart> _repository;

        public DeleteCartCommandHandler(IRepository<Domain.Entities.Cart> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;
            await _repository.DeleteAsync(request.id);
            return true;
        }
    }
}
