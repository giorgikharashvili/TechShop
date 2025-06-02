using MediatR;

namespace TechShop.Application.Features.Users.UpdateUsers
{
    public record UpdateUsersCommand(
        int Id,
        string FirstName,
        string LastName,
        string Username,
        string Email,
        string Password,
        string PhoneNumber
        ) : IRequest<bool>;
}
