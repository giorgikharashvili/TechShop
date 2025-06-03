using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Wishlist.UpdateWishlist
{
    public class UpdateCartCommandHandler : IRequestHandler<UpdateWishlistCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Wishlist> _repository;
        private readonly IMapper _mapper;

        public UpdateCartCommandHandler(IRepository<Domain.Entities.Wishlist> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateWishlistCommand request, CancellationToken cancellationToken)
        {
            var wishlist = await _repository.GetByIdAsync(request.id);
            if (wishlist == null) return false;

            _mapper.Map(request, wishlist);

            wishlist.ModifiedAt = DateTime.UtcNow;
            wishlist.ModifiedBy = "System";

            await _repository.UpdateAsync(wishlist);

            return true;
        }
    }
}
