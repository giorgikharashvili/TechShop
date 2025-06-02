using MediatR;

namespace TechShop.Application.Features.Cart.UpdateCart
{
    

    public record UpdateCartCommand(
        int id,
        decimal TotalPrice
        ) : IRequest<bool>;
}
