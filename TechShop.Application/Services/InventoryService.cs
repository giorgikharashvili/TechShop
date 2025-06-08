using Microsoft.Extensions.Logging;
using Stripe;
using TechShop.Application.Services.Interfaces;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Services;

public class InventoryService(
    IProductSkuRepository _productSkuRepository,
    ILogger<InventoryService> _logger) : IInventoryService
{
    public async Task UpdateStockFromLineItemsAsync(IEnumerable<LineItem> lineItems)
    {
        foreach (var item in lineItems)
        {
            var skuKey = item.Price?.LookupKey;

            if (string.IsNullOrWhiteSpace(skuKey))
            {
                _logger.LogWarning("SKU lookup key missing.");
                continue;
            }

            var sku = await _productSkuRepository.GetBySkuAsync(skuKey);

            if (sku != null && int.TryParse(sku.StockQuantity, out int currentStock))
            {
                var quantity = item.Quantity ?? 1;
                var newStock = Math.Max(currentStock - (int)quantity, 0);
                sku.StockQuantity = newStock.ToString();

                await _productSkuRepository.UpdateAsync(sku);
                _logger.LogInformation("Updated stock for SKU {Sku}: {NewStock}", skuKey, newStock);
            }
            else
            {
                _logger.LogWarning("SKU not found or invalid stock: {Sku}", skuKey);
            }
        }
    }
}
