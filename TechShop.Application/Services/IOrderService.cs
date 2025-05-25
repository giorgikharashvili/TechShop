using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.TechShop.Domain.Entites;

namespace TechShop.Application.Services
{
    internal interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetById(int id);
        Order PostOrder(Order order);
        void DeleteOrder(int id);
        void UpdateOrder(int id, Order order);
    }
}
