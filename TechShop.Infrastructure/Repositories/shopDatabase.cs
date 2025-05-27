using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Infrastructure.Repositories
{
    public class shopDatabase
    {
        public List<Users> Users { get; set; } = new();
        public List<Addresses> Addresses { get; set; } = new();
        public List<Cart> Carts { get; set; } = new();
        public List<CartItem> CartItems { get; set; } = new();
        public List<Categories> Categories { get; set; } = new();
        public List<OrderDetails> Orders { get; set; } = new();
        public List<OrderItem> OrderItems { get; set; } = new();
        public List<Payments> Payments { get; set; } = new();
        public List<Products> Products { get; set; } = new();
        public List<ProductsSkus> ProductsSkus { get; set; } = new();
        public List<ProductSkuAttributes> ProductSkuAttributes { get; set; } = new();
        public List<Wishlist> Wishlists { get; set; } = new();
    }
}
