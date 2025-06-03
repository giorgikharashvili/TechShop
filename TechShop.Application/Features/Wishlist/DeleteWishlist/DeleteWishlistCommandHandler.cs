using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Wishlist.DeleteWishlist
{
    public class DeleteWishlistCommandHandler : IRequestHandler<DeleteWishlistCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Wishlist> _repository;

        public DeleteWishlistCommandHandler(IRepository<Domain.Entities.Wishlist> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteWishlistCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;

            await _repository.DeleteAsync(request.id);

            return true;
        }
    }
}
