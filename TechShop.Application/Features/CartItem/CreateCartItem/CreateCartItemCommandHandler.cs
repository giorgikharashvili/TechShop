using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.CartItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.CartItem.CreateCartItem
{
    public class CreateCartItemCommandHandler(
        IRepository<Domain.Entities.CartItem> _repository,
        IMapper _mapper,
        ILogger<CreateCartItemCommandHandler> _logger) : IRequestHandler<CreateCartItemCommand, CartItemDto>
    {
        public async Task<CartItemDto> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateCartItemCommand for CartId: {CartId}", request.Dto.CartId);

            var entity = _mapper.Map<Domain.Entities.CartItem>(request.Dto);
            await _repository.AddAsync(entity);

            _logger.LogInformation("CartItem created and saved for CartId: {CartId}", request.Dto.CartId);

            var dto = _mapper.Map<CartItemDto>(entity);

            return dto;
        }
    }
}
