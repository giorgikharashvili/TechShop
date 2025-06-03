using FluentValidation;
using TechShop.Domain.DTOs.Users;

namespace TechShop.Application.Features.Users.CreateUsers
{
    public class CreateUsersValidator  : AbstractValidator<UserDto>
    {
        public CreateUsersValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
