using MediatR;

namespace TechShop.Application.Features.OrderDetails.DeleteOrderDetails
{
    public record DeleteOrderDetailsCommand(int id) : IRequest<bool>;
}
