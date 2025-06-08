using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Cart;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Cart.CreateCart;

public class CreateCartCommandHandler(
    IRepository<Domain.Entities.Cart> _repository,
    IMapper _mapper,
    ILogger<CreateCartCommandHandler> _logger
    ) : IRequestHandler<CreateCartCommand, CartDto>
{
    public async Task<CartDto> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateCartCommand for UserId: {UserId}", request.Dto.UserId);

        var entity = _mapper.Map<Domain.Entities.Cart>(request.Dto);
        await _repository.AddAsync(entity);

        _logger.LogInformation("Cart created and saved for UserId: {UserId}", request.Dto.UserId);

        var dto = _mapper.Map<CartDto>(entity);

        return dto;
    }
}
