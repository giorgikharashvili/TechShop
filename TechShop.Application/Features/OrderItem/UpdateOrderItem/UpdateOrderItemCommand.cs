using MediatR;
using TechShop.Domain.DTOs.OrderItem;

namespace TechShop.Application.Features.OrderItem.UpdateOrderItem
{
    public record UpdateOrderItemCommand(int id,UpdateOrderItemDto Dto) : IRequest<bool>;
}
