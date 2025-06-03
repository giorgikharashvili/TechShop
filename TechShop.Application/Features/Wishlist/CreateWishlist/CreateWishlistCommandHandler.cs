using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Wishlist;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Wishlist.CreateWishlist
{
    public class CreateWishlistCommandHandler : IRequestHandler<CreateWishlistCommand, WishlistDto>
    {
        private readonly IRepository<Domain.Entities.Wishlist> _repository;
        private readonly IMapper _mapper;
        
        public CreateWishlistCommandHandler(IRepository<Domain.Entities.Wishlist> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<WishlistDto> Handle(CreateWishlistCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Wishlist>(request.Dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(entity);

            var dto = _mapper.Map<WishlistDto>(entity);

            return dto;
        }
    }
}
