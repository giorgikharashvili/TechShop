using MediatR;

namespace TechShop.Application.Features.OrderItem.UpdateOrderItem
{
    public record UpdateOrderItemCommand(
        int Id,
        int ProductId,
        int ProductSkuId,
        int Quantity
        ) : IRequest<bool>;
}
