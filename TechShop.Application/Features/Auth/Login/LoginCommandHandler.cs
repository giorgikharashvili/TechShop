using MediatR;
using TechShop.Application.Services.Interfaces;

namespace TechShop.Application.Features.Auth.Login;
    public class LoginCommandHandler(IAuthService _authService) : IRequestHandler<LoginCommand, string>
    {
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.AuthenticateAsync(request.Email, request.Password);
        }
    }

