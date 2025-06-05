﻿using MediatR;

namespace TechShop.Application.Features.Auth.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<string>;
}
