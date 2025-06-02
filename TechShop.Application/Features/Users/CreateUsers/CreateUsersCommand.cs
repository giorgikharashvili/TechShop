using MediatR;
using TechShop.Domain.DTOs.Users;

namespace TechShop.Application.Features.Users.CreateUsers
{
    public record CreateUsersCommand(
        string FirstName,
        string LastName,
        string Username,
        string Email,
        string Password,
        string PhoneNumber
        ) : IRequest<UserDto>;
    
}
