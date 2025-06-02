using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Wishlist;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Wishlist.GetAllWishlist
{
    public class GetAllWishlistQueryHandler : IRequestHandler<GetAllWishlistQuery, IEnumerable<WishlistDto>>
    {
        private readonly IRepository<Domain.Entities.Wishlist> _repository;
        private readonly IMapper _mapper;

        public GetAllWishlistQueryHandler(IRepository<Domain.Entities.Wishlist> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<WishlistDto>> Handle(GetAllWishlistQuery request, CancellationToken cancellationToken)
        {
            var wishlist = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<WishlistDto>>(wishlist);
        }
    }
}
