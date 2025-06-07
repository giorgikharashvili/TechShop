using MediatR;
using TechShop.Domain.DTOs.Register;

namespace TechShop.Application.Features.Auth.Register
{
    public record RegisterCommand(RegisterDto Dto) : IRequest<string>;
}
