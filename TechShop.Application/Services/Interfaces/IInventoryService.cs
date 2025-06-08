using Stripe;

namespace TechShop.Application.Services.Interfaces;

public interface IInventoryService
{
    Task UpdateStockFromLineItemsAsync(IEnumerable<LineItem> lineItems);
}
