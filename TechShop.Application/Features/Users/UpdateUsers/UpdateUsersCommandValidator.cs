using FluentValidation;

namespace TechShop.Application.Features.Users.UpdateUsers
{
    public class UpdateUsersCommandValidator :  AbstractValidator<UpdateUsersCommand>
    {
        public UpdateUsersCommandValidator()
        {
            RuleFor(x => x.Dto.Username).NotEmpty();
            RuleFor(x => x.Dto.PhoneNumber).NotEmpty();
            RuleFor(x => x.Dto.Password).NotEmpty();
        }
    }
}
