using MediatR;
using TechShop.Domain.DTOs.OrderItem;

namespace TechShop.Application.Features.OrderItem.CreateOrderItem
{
    public record CreateOrderItemCommand(
        int ProductId,
        int ProductSkuId,
        int Quantity
        ) : IRequest<OrderItemDto>;
    
}
