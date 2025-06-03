using MediatR;
using TechShop.Domain.DTOs.Users;

namespace TechShop.Application.Features.Users.CreateUsers
{
    public record CreateUsersCommand(CreateUserDto Dto) : IRequest<UserDto>;
    
}
