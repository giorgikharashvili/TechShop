using MediatR;
using TechShop.Domain.DTOs.OrderDetails;

namespace TechShop.Application.Features.OrderDetails.CreateOrderDetails
{
    public record CreateOrderDetailsCommand(CreateOrderDetailsDto Dto) : IRequest<OrderDetailsDto>;
    
}
