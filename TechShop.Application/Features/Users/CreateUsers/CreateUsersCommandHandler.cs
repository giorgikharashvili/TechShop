using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TechShop.Domain.DTOs.Users;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;


namespace TechShop.Application.Features.Users.CreateUsers
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand, UserDto>
    {
        private readonly IRepository<Domain.Entities.Users> _repository;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<Domain.Entities.Users> _passwordHasher;
        
        public CreateUsersCommandHandler(IRepository<Domain.Entities.Users> repository, IMapper mapper, PasswordHasher<Domain.Entities.Users> passwordHasher)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }


        public async Task<UserDto> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Users>(request.Dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.PasswordHash = _passwordHasher.HashPassword(entity, request.Dto.Password);

            await _repository.AddAsync(entity);

            var dto = _mapper.Map<UserDto>(entity);

            return dto;
        }
    }
}
