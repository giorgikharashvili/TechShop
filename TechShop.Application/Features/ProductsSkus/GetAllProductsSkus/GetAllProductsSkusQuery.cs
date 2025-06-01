using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.ProductsSkus;

namespace TechShop.Application.Features.Address.GetAllProductsSkus
{
    public record GetAllProductsSkusQuery() : IRequest<IEnumerable<ProductsSkusDto>>;
  
}
