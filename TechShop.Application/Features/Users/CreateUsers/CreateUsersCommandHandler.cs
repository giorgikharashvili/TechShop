using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Users;
using TechShop.Infrastructure.Repositories.Interfaces;


namespace TechShop.Application.Features.Users.CreateUsers
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand, UserDto>
    {
        private readonly IRepository<Domain.Entities.Users> _repository;
        private readonly IMapper _mapper;
        
        public CreateUsersCommandHandler(IRepository<Domain.Entities.Users> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<UserDto> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Users>(request.Dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(entity);

            var dto = _mapper.Map<UserDto>(entity);

            return dto;
        }
    }
}
