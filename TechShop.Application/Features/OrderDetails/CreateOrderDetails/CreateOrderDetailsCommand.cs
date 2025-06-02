using MediatR;
using TechShop.Domain.DTOs.OrderDetails;

namespace TechShop.Application.Features.OrderDetails.CreateOrderDetails
{
    public record CreateOrderDetailsCommand(
       int UserId,
       decimal TotalPrice
        ) : IRequest<OrderDetailsDto>;
    
}
