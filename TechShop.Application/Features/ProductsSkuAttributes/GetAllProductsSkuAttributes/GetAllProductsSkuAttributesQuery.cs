using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.ProductsSkuAttributes;

namespace TechShop.Application.Features.Address.GetAllProductsSkuAttributes
{
    public record GetAllProductsSkuAttributesQuery() : IRequest<IEnumerable<ProductSkuAttributesDto>>;
  
}
