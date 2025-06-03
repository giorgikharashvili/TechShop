using MediatR;
using TechShop.Domain.DTOs.OrderItem;

namespace TechShop.Application.Features.OrderItem.GetOrderItemById
{
    public record GetOrderItemByIdQuery(int id) : IRequest<OrderItemDto?>;
}
