using MediatR;
using TechShop.Domain.DTOs.OrderDetails;


namespace TechShop.Application.Features.OrderDetails.UpdateOrderDetails
{
    public record UpdateOrderDetailsCommand(int id,UpdateOrderDetailsDto Dto) : IRequest<bool>;
}
