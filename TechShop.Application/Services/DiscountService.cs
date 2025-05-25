using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.TechShop.Domain.Entites;

namespace TechShop.Application.Services
{
    internal class DiscountService : IDiscountService
    {
        private static List<Discount> Discounts = new List<Discount>
        {
            new Discount { Id = 1, ProductId = 1, DiscountAmount = 500m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
            new Discount { Id = 2, ProductId = 2, DiscountAmount = 300m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2) }
        };

        public void DeleteDiscount(int id)
        {
            var discount = Discounts.FirstOrDefault(d => d.Id == id);
            if (discount != null) Discounts.Remove(discount);
        }

        public IEnumerable<Discount> GetAllDiscounts()
        {
            return Discounts;
        }

        public Discount GetById(int id)
        {
            var discount = Discounts.FirstOrDefault(d => d.Id == id);
            if (discount == null) throw new KeyNotFoundException($"Discount with {id} Id was not found");
            return discount;
        }

        public Discount PostDiscount(Discount discount)
        {
            discount.Id = Discounts.Count > 0 ? Discounts.Max(c => c.Id) + 1 : 1;
            Discounts.Add(discount);
            return discount;
        }

        public void UpdateDiscount(int id, Discount discount)
        {
            var existingDiscount = Discounts.FirstOrDefault(d => d.Id == id);
            if(existingDiscount == null) throw new KeyNotFoundException($"Discount with {id} Id was not found");

            existingDiscount.ProductId = discount.ProductId;
            existingDiscount.DiscountAmount = discount.DiscountAmount;
            existingDiscount.StartDate = discount.StartDate;
            existingDiscount.EndDate = discount.EndDate;
        }
    }
}
