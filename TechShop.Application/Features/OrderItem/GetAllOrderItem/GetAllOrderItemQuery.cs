using MediatR;
using TechShop.Domain.DTOs.OrderItem;

namespace TechShop.Application.Features.OrderItem.GetAllOrderItem
{
    public record GetAllOrderItemQuery() : IRequest<IEnumerable<OrderItemDto>>;
  
}
