using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Application.Services.Interfaces;

namespace TechShop.Application.Features.Auth.Login;

public class LoginCommandHandler(
    IAuthService _authService,
    ILogger<LoginCommandHandler> _logger
    ) : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling LoginCommand for Email: {Email}", request.Email);

        var token = await _authService.AuthenticateAsync(request.Email, request.Password);

        _logger.LogInformation("Authentication completed for Email: {Email}", request.Email);

        return token;
    }
}
