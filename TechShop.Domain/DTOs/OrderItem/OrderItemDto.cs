﻿namespace TechShop.Domain.DTOs.OrderItem
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductsSkuId { get; set; }
        public int Quantity { get; set; }
    }
}
