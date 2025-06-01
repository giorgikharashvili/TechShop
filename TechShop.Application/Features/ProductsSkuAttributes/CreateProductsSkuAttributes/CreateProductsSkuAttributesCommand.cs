using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TechShop.Application.Features.Address.CreateProductsSkuAttributes
{
    public record CreateProductsSkuAttributesCommand(
        string AddressLine1,
        string AddressLine2,
        string Country,
        string City,
        string PostalCode,
        int UserId
        ) : IRequest<int>;
    
}
