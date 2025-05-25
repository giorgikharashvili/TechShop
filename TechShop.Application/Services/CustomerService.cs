using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechShop.TechShop.Domain.Entites;

namespace TechShop.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private static List<Customer> Customers = new List<Customer>
        {
            new Customer { Id = 1, Email = "johndoe@gmail.com", FullName="John Doe"},
            new Customer { Id = 2, Email = "giorgikharashvili@gmail.com", FullName="Giorgi Kharashvili"}
        };



        public void DeleteCustomer(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) throw new KeyNotFoundException($"Customer with {id} Id was not found");
            Customers.Remove(customer);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return Customers;
        }

        public Customer GetById(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) throw new KeyNotFoundException($"Customer with {id} Id was not found");
            return customer;
        }

        public Customer PostCustomer(Customer customer)
        {
            customer.Id = Customers.Count > 0 ? Customers.Max(c => c.Id) + 1 : 1;
            Customers.Add(customer);

            return customer;
        }

        public void UpdateCustomer(int id, Customer customer)
        {
            var existingCustomer = Customers.FirstOrDefault(c => c.Id == id);
            if (existingCustomer == null) throw new KeyNotFoundException($"Customer with {id} Id was not found");
            existingCustomer.Email = customer.Email;
            existingCustomer.FullName = customer.FullName;
        }
    }
}
