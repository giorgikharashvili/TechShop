using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Wishlist;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Wishlist.GetWishlistById
{
    public class GetWishlistByIdQueryHandler : IRequestHandler<GetWishlistByIdQuery, WishlistDto?>
    {
        private readonly IRepository<Domain.Entities.Wishlist> _repository;
        private readonly IMapper _mapper;

        public GetWishlistByIdQueryHandler(IRepository<Domain.Entities.Wishlist> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<WishlistDto?> Handle(GetWishlistByIdQuery request, CancellationToken cancellationToken)
        {
            var wishlist = await _repository.GetByIdAsync(request.id);
            if (wishlist == null) return null;
            return _mapper.Map<WishlistDto>(wishlist);
        }
    }
}
