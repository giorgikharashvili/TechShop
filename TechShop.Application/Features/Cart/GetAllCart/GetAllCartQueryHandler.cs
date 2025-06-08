using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Cart;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Cart.GetAllCart;

public class GetAllCartQueryHandler(
    IRepository<Domain.Entities.Cart> _repository,
    IMapper _mapper,
    ILogger<GetAllCartQueryHandler> _logger
    ) : IRequestHandler<GetAllCartQuery, IEnumerable<CartDto>>
{
    public async Task<IEnumerable<CartDto>> Handle(GetAllCartQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllCartQuery");

        var cart = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} carts from repository", cart.Count());

        return _mapper.Map<IEnumerable<CartDto>>(cart);
    }
}
