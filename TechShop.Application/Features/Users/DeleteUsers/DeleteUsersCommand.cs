using MediatR;

namespace TechShop.Application.Features.Users.DeleteUsers
{
    public record DeleteUsersCommand(int id) : IRequest<bool>;
}
