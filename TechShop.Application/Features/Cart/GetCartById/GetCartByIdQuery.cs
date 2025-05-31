using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetAddressesById
{
    public record GetAddressesByIdQuery(int id) : IRequest<AddressesDto?>;
}
