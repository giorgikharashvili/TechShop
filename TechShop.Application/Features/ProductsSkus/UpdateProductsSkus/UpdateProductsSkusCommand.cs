using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.UpdateProductsSkus
{
    

    public record UpdateProductsSkusCommand(
        int id,
        string AddressLine1, 
        string AddressLine2, 
        string Country, 
        string City, 
        string PostalCode
        ) : IRequest<bool>;
}
