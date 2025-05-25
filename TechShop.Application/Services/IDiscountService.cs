using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.TechShop.Domain.Entites;

namespace TechShop.Application.Services
{
    internal interface IDiscountService
    {
        IEnumerable<Discount> GetAllDiscounts();
        Discount GetById(int id);
        Discount PostDiscount(Discount discount);
        void DeleteDiscount(int id);
        void UpdateDiscount(int id, Discount discount);
    }
}
