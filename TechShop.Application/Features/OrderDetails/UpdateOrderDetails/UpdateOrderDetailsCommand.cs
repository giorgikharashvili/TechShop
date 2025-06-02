using MediatR;


namespace TechShop.Application.Features.OrderDetails.UpdateOrderDetails
{
    public record UpdateOrderDetailsCommand(
        int id,
        int UserId,
        decimal TotalPrice
        ) : IRequest<bool>;
}
