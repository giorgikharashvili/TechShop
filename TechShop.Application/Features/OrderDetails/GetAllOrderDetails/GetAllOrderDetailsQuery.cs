using MediatR;
using TechShop.Domain.DTOs.OrderDetails;

namespace TechShop.Application.Features.OrderDetails.GetAllOrderDetails
{
    public record GetAllOrderDetailsQuery() : IRequest<IEnumerable<OrderDetailsDto>>;
  
}
