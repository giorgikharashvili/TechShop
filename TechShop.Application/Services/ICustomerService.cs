using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.TechShop.Domain.Entites;

namespace TechShop.Application.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetById(int id);
        Customer PostCustomer(Customer customer);
        void DeleteCustomer(int id);
        void UpdateCustomer(int id, Customer customer);
    }
}
