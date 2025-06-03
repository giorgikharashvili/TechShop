using MediatR;
using TechShop.Domain.DTOs.OrderItem;

namespace TechShop.Application.Features.OrderItem.CreateOrderItem
{
    public record CreateOrderItemCommand(CreateOrderItemDto Dto) : IRequest<OrderItemDto>;
    
}
