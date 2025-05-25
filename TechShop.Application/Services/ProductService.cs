using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.TechShop.Domain.Entites;

namespace TechShop.Application.Services
{


    public class ProductService : IProductService
    {
        private static List<Product> Products = new List<Product>
        {
            new Product 
            { 
                Id = 1,
                Name = "Alienware Rtx 4090 I9 32GB Ram",
                Description = "Gaming PC for latest games",
                StockQuantity = 10, Price = 5000,
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "HyperX Alloy Origins 60 Mechaincal Keyboard",
                Description = "Hyperx Mechanical Switches Petite 60% form factor",
                StockQuantity=30,
                Price = 99,
                CategoryId = 3
            }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return Products;
        }

        public void DeleteProduct(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if(product == null) throw new KeyNotFoundException($"Product with {id} Id was not found");
            Products.Remove(product);
        }

        public Product GetById(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if(product == null) throw new KeyNotFoundException($"Product with {id} Id was not found");

            return product;
        }

        public Product PostProduct(Product product)
        {
            // Since no database to auto-generate ids
            product.Id = Products.Count > 0 ? Products.Max(c => c.Id) + 1 : 1;
            Products.Add(product);
            return product;
        }

        public void UpdateProduct(int id, Product product)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null) throw new KeyNotFoundException($"Product with {id} Id was not found");

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;

        }
    }
}
