using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Users;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Users.GetUsersById
{
    public class GetUsersByIdQueryHandler : IRequestHandler<GetUsersByIdQuery, UserDto?>
    {
        private readonly IRepository<Domain.Entities.Users> _repository;
        private readonly IMapper _mapper;

        public GetUsersByIdQueryHandler(IRepository<Domain.Entities.Users> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDto?> Handle(GetUsersByIdQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetByIdAsync(request.id);
            if (users == null) return null;
            return _mapper.Map<UserDto>(users);
        }
    }
}
