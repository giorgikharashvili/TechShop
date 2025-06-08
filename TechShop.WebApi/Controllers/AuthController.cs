using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechShop.Application.Features.Auth.Register;
using TechShop.Application.Features.Auth.Login;

namespace TechShop.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator _mediator, ILogger<AuthController> _logger) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        _logger.LogInformation("Registration attempt for Email: {Email}", command.Dto.Email);

        try
        {
            var token = await _mediator.Send(command);
            _logger.LogInformation("User registered successfully: {Email}", command.Dto.Email);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registration failed for Email: {Email}", command.Dto.Email);
            return BadRequest("Registration failed");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        _logger.LogInformation("Login attempt for Email: {Email}", command.Email);

        try
        {
            var token = await _mediator.Send(command);
            _logger.LogInformation("Login successful for Email: {Email}", command.Email);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Login failed for Email: {Email}", command.Email);
            return Unauthorized("Login failed");
        }
    }
}
