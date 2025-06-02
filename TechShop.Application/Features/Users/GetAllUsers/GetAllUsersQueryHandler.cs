using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Users;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Users.GetAllUsers
{
    public class GetAllCartQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IRepository<Domain.Entities.Users> _repository;
        private readonly IMapper _mapper;

        public GetAllCartQueryHandler(IRepository<Domain.Entities.Users> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
