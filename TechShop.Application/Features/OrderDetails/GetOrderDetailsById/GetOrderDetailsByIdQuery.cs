using MediatR;
using TechShop.Domain.DTOs.OrderDetails;

namespace TechShop.Application.Features.OrderDetails.GetOrderDetailsById
{
    public record GetOrderDetailsByIdQuery(int id) : IRequest<OrderDetailsDto?>;
}
