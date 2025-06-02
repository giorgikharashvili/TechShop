using MediatR;
using TechShop.Domain.DTOs.Users;

namespace TechShop.Application.Features.Users.GetAllUsers
{
    public record GetAllUsersQuery() : IRequest<IEnumerable<UserDto>>;
  
}
