using MediatR;
using TechShop.Domain.DTOs.Users;

namespace TechShop.Application.Features.Users.UpdateUsers
{
    public record UpdateUsersCommand(int id, UpdateUserDto Dto) : IRequest<bool>;
}
