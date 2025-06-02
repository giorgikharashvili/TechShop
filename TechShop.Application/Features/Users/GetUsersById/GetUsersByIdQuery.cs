using MediatR;
using TechShop.Domain.DTOs.Users;

namespace TechShop.Application.Features.Users.GetUsersById
{
    public record GetUsersByIdQuery(int id) : IRequest<UserDto?>;
}
