using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;

namespace TechShop.Infrastructure.Repositories.Interfaces;

public interface IOrderDetailsRepository
{
    Task<int> AddAsync(OrderDetails orderDetails);
}
