using MediatR;

namespace TechShop.Application.Features.OrderItem.DeleteOrderItem
{
    public record DeleteOrderItemCommand(int id) : IRequest<bool>;
}
